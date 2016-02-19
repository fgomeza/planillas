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
            SessionManager sm = SessionManager.getInstance();
            ViewManager vm = ViewManager.getInstance();
            if (!sm.isLogged(Session))
            {
                return View("error");
            }
            User user = sm.getUser(Session);
            if (!sm.verifyOperation(user, group, operation)){
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