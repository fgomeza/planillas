using SistemaDePlanillas.Filters;
using SistemaDePlanillas.Models;
using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Linq;

namespace SistemaDePlanillas.Controllers
{
    [Authorize]
    [PermissionCheck]
    public class OperationsApiController : ApiController
    {
        
        public JsonResult<Response> Post(string group, string operation, [FromBody]object[] args)
        {
            try
            {
                if (args == null)
                {
                    args = new object[0];
                }

                args=args.Select(o => o is string && o.Equals("NOW") ? (object)DateTime.Now : o).ToArray();
                //gets the session state
                var Session = HttpContext.Current.Session;

                SessionManager sm = SessionManager.Instance;

                //checks if the user is logged
                if (!sm.isLogged(Session))
                {
                    return Json(Responses.Error(10, "No se ha iniciado sesion"));
                }

                User user = sm.getUser(Session);   

                //add the user to the parameters   
                object[] parameters = new object[args.Length + 1];
                parameters[0] = user;
                System.Array.Copy(args, 0, parameters, 1, args.Length);
                //types array for calling the correct overload of the method
                Type[] paramsTypes = parameters.Select(p => p.GetType()).ToArray();

                //formatting the class name
                string groupType = "SistemaDePlanillas.Models.Operations." + group + "Group";

                //Uses reflexion to get the correct method
                Type type = Type.GetType(groupType, false, true);
                if (type == null)
                {
                    return Json(Responses.Error(12, "No se encuentra el grupo: " + group + ", imposible realizar operacion"));
                }
                MethodInfo method = type.GetMethod(operation, BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase, null, paramsTypes, null);
                if (method == null)
                {
                    return Json(Responses.Error(13, "No se encuentra la operacion: " + group + "/" + operation));
                }
                
                //call the method
                return Json((Response)method.Invoke(null, parameters));
            }
            catch (TargetParameterCountException)
            {
                return Json(Responses.Error(14, "No coincide el numero de parametros esperado para: " + group + "/" + operation));
            }
            catch (ArgumentException)
            {
                return Json(Responses.Error(15, "No coincide el tipo de los argumentos esperados para: " + group + "/" + operation));
            }
            catch (Exception e)
            {
                return Json(Responses.ExceptionError(e));
            }
        }

        [Authorize]
        [PermissionCheck]
        public JsonResult<Response> Post(string group, string operation, string call, [FromBody]object[] args)
        {
            try
            {
                if (args == null)
                {
                    args = new object[0];
                }
                args = args.Select(o => o is string && o.Equals("NOW") ? (object)DateTime.Now : o).ToArray();
                //gets the session state
                var Session = HttpContext.Current.Session;

                SessionManager sm = SessionManager.Instance;

                //checks if the user is logged
                if (!sm.isLogged(Session))
                {
                    return Json(Responses.Error(10, "No se ha iniciado sesion"));
                }

                User user = sm.getUser(Session);

                //add the user to the parameters   
                object[] parameters = new object[args.Length + 1];
                parameters[0] = user;
                System.Array.Copy(args, 0, parameters, 1, args.Length);
                //types array for calling the correct overload of the method
                Type[] paramsTypes = parameters.Select(p => p.GetType()).ToArray();

                //formatting the class name
                string groupType = "SistemaDePlanillas.Models.Operations." + group + "Group";
                operation = operation + "_" + call;

                //Uses reflexion to get the correct method
                Type type = Type.GetType(groupType, false, true);
                if (type == null)
                {
                    return Json(Responses.Error(12, "No se encuentra el grupo: " + group + ", imposible realizar operacion"));
                }
                MethodInfo method = type.GetMethod(operation, BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase, null, paramsTypes, null);             
                if (method == null)
                {
                    return Json(Responses.Error(13, "No se encuentra la operacion: " + group + "/" + operation));
                }

                //call the method
                return Json((Response)method.Invoke(null, parameters));
            }
            catch (TargetParameterCountException)
            {
                return Json(Responses.Error(14, "No coincide el numero de parametros esperado para: " + group + "/" + operation + "/" + call));
            }
            catch (ArgumentException)
            {
                return Json(Responses.Error(15, "No coincide el tipo de los argumentos esperados para: " + group + "/" + operation + "/" + call));
            }
            catch (Exception e)
            {
                return Json(Responses.ExceptionError(e));
            }
        }

    }
}
