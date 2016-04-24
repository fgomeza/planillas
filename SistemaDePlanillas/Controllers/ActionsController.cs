using SistemaDePlanillas.Filters;
using SistemaDePlanillas.Models;
using System;
using System.Reflection;
using System.Web.Http;
using System.Linq;
using System.Net;
using System.Collections.Generic;

namespace SistemaDePlanillas.Controllers
{

    public class ActionsController : ApiController
    {
        private readonly string OperationsNamespace = "SistemaDePlanillas.Models.Operations";
        private readonly IActionsEventsListener listener = new ActionsEvents();

        [Authorize]
        [PermissionCheck]
        public Response fromCall(string group, string operation, [FromBody]object[] args)
        {
            return doAction(group, operation, args);
        }

        [Authorize]
        [PermissionCheck]
        public Response fromCall(string group, string operation, string call, [FromBody]object[] args)
        {
            return doAction(group, operation + "_" + call, args);
        }

        [Authorize]
        [PermissionCheck]
        public Response fromForm(string group, string operation, [FromBody]IDictionary<string, object> args)
        {
            return doAction(group, operation, args);
        }

        [Authorize]
        [PermissionCheck]
        public Response fromForm(string group, string operation, string call, [FromBody]IDictionary<string, object> args)
        {
            return doAction(group, operation + "_" + call, args);
        }

        private Response doAction(string group, string operation, object[] args)
        {
            if (args == null) args = new object[0];
            DateTime callTime = DateTime.Now;

            //gets the session user
            User user = SessionManager.Instance.getSessionUser(RequestContext);

            //add the user to the parameters   
            object[] parameters = new object[args.Length + 1];
            parameters[0] = user;
            System.Array.Copy(args, 0, parameters, 1, args.Length);

            //types array for calling the correct overload of the method
            Type[] paramsTypes = parameters.Select(p => p.GetType()).ToArray();

            //formatting the class name
            string groupType = string.Format("{0}.{1}Group", OperationsNamespace, group);

            //Uses reflexion to get the group
            Type type = Type.GetType(groupType, false, true);
            if (type == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            //Uses reflexion to get the correct method  
            MethodInfo method = type.GetMethod(operation, BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase, null, paramsTypes, null);
            if (method == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            //map args as key/value dictionary for listeners pleasure
            var argsMap = method.GetParameters().Zip(parameters, (k, v)=> new { k, v }).ToDictionary(x => x.k.Name, x => x.v);

            try
            {
                //call the method
                Response response = (Response)method.Invoke(null, parameters);
                listener.OnActionComplete(response,user, callTime, group, operation, argsMap);
                return response;
            }
            catch (Exception e) when (e.InnerException is AppException)
            {
                var ex = e.InnerException as AppException;
                Response response = Responses.AplicationError(ex.Code, ex.DescriptionError);
                listener.OnActionError(response, user, callTime, group, operation, argsMap, ex.Code, ex.DescriptionError);
                return response;
            }
            catch (Exception e)
            {
                listener.OnException(user, callTime, group, operation, argsMap, e.InnerException);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



        private Response doAction(string group, string operation, IDictionary<string, object> args)
        {
            if (args == null) args = new Dictionary<string, object>(1);
            DateTime callTime = DateTime.Now;

            //gets the session user
            User user = SessionManager.Instance.getSessionUser(RequestContext);

            //add the user to the parametters
            args["user"] = user;

            //formatting the class name
            string groupType = string.Format("{0}.{1}Group", OperationsNamespace, group);

            //Uses reflexion to get the group
            Type type = Type.GetType(groupType, false, true);
            if (type == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            //Uses reflexion to get the firts method that matches the parametters names
            MethodInfo method = type.GetMethods().FirstOrDefault(m =>
            m.Name == operation &&
            m.GetParameters().Length == args.Count &&
            m.GetParameters().All(p => args.ContainsKey(p.Name)
            ));
            if (method == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            try
            {
                //Builds the parametters array casting to the especific required types if necesary
                object[] parameters = method.GetParameters().Select(p =>
                {
                    var val = args[p.Name];
                    return val is IConvertible ? Convert.ChangeType(val, p.ParameterType) : val;
                }).ToArray();

                //call the method
                Response response = (Response)method.Invoke(null, parameters);
                listener.OnActionComplete(response, user, callTime, group, operation, args);
                return response;
            }
            catch (Exception e) when (e is FormatException || e is InvalidCastException || e is ArgumentException)
            {
                listener.OnBadActionArguments(user, callTime, group, operation, args, method.GetParameters());
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (Exception e) when (e.InnerException is AppException)
            {
                var ex = e.InnerException as AppException;
                Response response = Responses.AplicationError(ex.Code, ex.DescriptionError);
                listener.OnActionError(response, user, callTime, group, operation, args, ex.Code, ex.DescriptionError);
                return response;
            }
            catch (Exception e)
            {
                listener.OnException(user, callTime, group, operation, args, e.InnerException);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }

    }
}
