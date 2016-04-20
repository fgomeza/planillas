using SistemaDePlanillas.Filters;
using SistemaDePlanillas.Models;
using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Linq;
using System.Web.Security;

namespace SistemaDePlanillas.Controllers
{

    public class OperationsApiController : ApiController
    {
        readonly string OperationsNamespace = "SistemaDePlanillas.Models.Operations";

        [Authorize]
        [PermissionCheck]
        public JsonResult<Response> Post(string group, string operation, [FromBody]object[] args)
        {
            return Json(doAction(group, operation, args));
        }

        [Authorize]
        [PermissionCheck]
        public JsonResult<Response> Post(string group, string operation, string call, [FromBody]object[] args)
        {
            return Json(doAction(group, operation + "_" + call, args));
        }

        private Response doAction(string group, string action, object[] args)
        {
            if (args == null) args = new object[0];
            DateTime callTime = DateTime.Now;
            User user = null;
            try
            {
                //gets the session user
                user = SessionManager.Instance.getSessionUser(RequestContext);

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
                if (type == null)
                {
                    return Responses.Error(12, "No se encuentra el grupo: " + group + ", imposible realizar operacion");
                }

                //Uses reflexion to get the correct method
                MethodInfo method = type.GetMethod(action, BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase, null, paramsTypes, null);
                if (method == null)
                {
                    return Responses.Error(13, "No se encuentra la operacion: " + group + "/" + action);
                }

                //call the method
                Response response = (Response)method.Invoke(null, parameters);
                Logger.Instance.LogAction(response, group, action, args, user, callTime);
                return response;
            }
            catch (TargetParameterCountException)
            {
                return Responses.Error(14, "No coincide el numero de parametros esperado para: " + group + "/" + action);
            }
            catch (ArgumentException)
            {
                return Responses.Error(15, "No coincide el tipo de los argumentos esperados para: " + group + "/" + action);
            }
            catch (Exception e)
            {
                Logger.Instance.LogActionError(e, group, action, args, user, callTime);
                return Responses.ExceptionError(e);
            }
        }
    }
}
