using SistemaDePlanillas.Models;
using SistemaDePlanillas.Models.ViewModels;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PlanillasFrontEnd.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller 
    {
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid && SessionManager.Instance.login(model.Username, model.Password, Session))
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                return RedirectToAction("Test", "Home");
            }
            else
            {
                /*Aquí hace falta una forma de tomar el error que sucedió
                */
                ModelState.AddModelError("LoginError", "Usuario o contraseña inválidos");
                return View();
            }

        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            SessionManager.Instance.logout(Session);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}