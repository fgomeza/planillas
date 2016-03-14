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
    [Authorize]
    public class AccountController : Controller 
    {
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
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
                ViewBag.Message = "RememberMe:" + model.RememberMe;
                return View();
            }

        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            SessionManager.Instance.logout(Session);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}