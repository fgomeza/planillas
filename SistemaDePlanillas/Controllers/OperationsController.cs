using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDePlanillas.Models;

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
            ViewData["config"] = new ViewConfig
            {
                title = vm.getOperationsGroup(group).desc,
                activeMenu = group,
                activeOption = operation,
                showNavbar = true,
                showMenubar = true
            };
            return View(group+"/"+operation);
        }

    }
}