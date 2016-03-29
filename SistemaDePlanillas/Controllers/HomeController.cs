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
        public ActionResult About()
        {
            ViewData["config"] = new ViewConfig
            {
                title = "Operaciones disponibles para el rol",
                showNavbar = true,
            };
            return View("index"); 
        }

        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}
