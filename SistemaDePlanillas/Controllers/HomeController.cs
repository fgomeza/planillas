using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDePlanillas.Models;

namespace SistemaDePlanillas.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}
