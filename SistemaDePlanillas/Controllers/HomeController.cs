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
        public string Index()
        {
            if (!SessionManager.getInstance().isLogged(Session))
            {
                SessionManager.getInstance().login("tutox", "pass123", Session);
            }
            return SessionManager.getInstance().getUser(Session).name+ ", home/about para ver barra de navegacion";
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

        public string holaMundo()
        {
            return "hola mundo";
        }
    }
}
