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
        public ActionResult Login(string returnUrl)
        {
            if(Request.IsAjaxRequest())
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ViewBag.returnUrl = returnUrl;
                return View();
            }

        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            //model.Username = model.Username.ToLower();
            if (ModelState.IsValid && SessionManager.Instance.login(model.Username, model.Password, Session))
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                return RedirectToLocal(returnUrl);
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


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}