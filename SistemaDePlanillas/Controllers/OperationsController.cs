using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDePlanillas.Models;
using System.Reflection;
using SistemaDePlanillas.Models.ViewModels;

namespace SistemaDePlanillas.Controllers
{
    public class OperationsController : Controller
    {
        // GET: Operations
        
        public ActionResult process(string group, string operation)
        {
            group = group.ToLower();
            operation = operation.ToLower();

            SessionManager sm = SessionManager.Instance;
            ViewManager vm = ViewManager.Instance;
            if (!sm.isLogged(Session))
            {
                return View("error");
            }
            User user = sm.getUser(Session);
            if (false && !sm.verifyOperation(user, group, operation)){
                return View("error");
            }
            //obtiene la clase (insencible a mayusculas)

            Type modelClass = Type.GetType("SistemaDePlanillas.Models.ViewModels." + group + operation + "ViewModel", false, true);         
            if (modelClass == null)
            {
                //Si no existe el model no se lo pasa a la vista
                return View(group + "/" + operation);
            }
            //(Sí existe el modelo) 
            object modelInstance;
            //busca constructor que reciba al usuario
            var constructor = modelClass.GetConstructor(new Type[] { user.GetType() });
            if (constructor != null)
            {
                //lo encontró
                modelInstance = constructor.Invoke(new object[] { user });
            }
            else
            {
                //no lo encontró (usa constructor sin parametros)
                modelInstance = modelClass.GetConstructor(Type.EmptyTypes).Invoke(null);
            }
            //le pasa la instancia del modelo a la vista
            return View(group+"/"+operation, modelInstance);
        }

    }
}