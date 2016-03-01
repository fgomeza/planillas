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
            if (!SessionManager.getInstance().isLogged(Session))
            {
                SessionManager.getInstance().login("JonnCh", "123", Session);
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

        public ActionResult Index()
        {
            Login();
            return Redirect("/test");
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}
