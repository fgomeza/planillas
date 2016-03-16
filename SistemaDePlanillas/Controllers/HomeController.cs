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
        public string Login()
        {
            if (!SessionManager.Instance.isLogged(Session))
            {
                SessionManager.Instance.login("JonnCh", "123", Session); 
            }
            return SessionManager.Instance.getUser(Session).Name+ ", home/about para ver barra de navegacion";
        }

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
            if(User.Identity.IsAuthenticated)
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
