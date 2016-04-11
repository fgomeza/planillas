using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDePlanillas.Models;
using SistemaDePlanillas.Filters;

namespace SistemaDePlanillas.Controllers
{
    public class HomeController : Controller
    {

        [SidebarFilter]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        [AjaxOnly]
        public ActionResult Dashboard()
        {
            return PartialView();
        }

        [AjaxOnly]
        public ActionResult Test()
        {
            return PartialView();
        }

    }
}
