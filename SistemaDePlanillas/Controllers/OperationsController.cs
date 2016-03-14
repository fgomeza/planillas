using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDePlanillas.Models;
using System.Reflection;

namespace SistemaDePlanillas.Controllers
{
    public class OperationsController : Controller
    {
        // GET: Operations
        
        public ActionResult process(string group, string operation)
        {
            SessionManager sm = SessionManager.Instance;
            ViewManager vm = ViewManager.Instance;
            if (!sm.isLogged(Session))
            {
                return View("error");
            }
            User user = sm.getUser(Session);
            if (!sm.verifyOperation(user, group, operation)){
                return View("error");
            }
            /*
            //obtiene la clase
            Type modelClass=Type.GetType("SistemaDePlanillas.Models.ViewModels." + group + "ViewModel");
            //crea la instancia pasandole el usuario al constructor
            object modelInstance=Activator.CreateInstance(type, new object[] { user });
            //le pasa el modelo a la vista
            return View(group+"/"+operation, modelInstance);
            */
            return View(group + "/" + operation);
        }

    }
}