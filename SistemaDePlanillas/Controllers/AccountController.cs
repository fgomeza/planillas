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
            ViewBag.returnUrl = returnUrl;
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            model.Username = model.Username.ToLower();
            User user = SessionManager.Instance.validateUser(model.Username, model.Password);
            if (ModelState.IsValid && user != null)
            {
                
                /* https://msdn.microsoft.com/en-us/library/system.web.security.formsauthenticationticket.aspx */

                string userData = string.Format("{0}|{1}", user.Location, user.Role);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    user.Name,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    model.RememberMe,
                    userData,
                    FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                // Redirect back to original URL.
                Response.Redirect(FormsAuthentication.GetRedirectUrl(model.Username, model.RememberMe));

                Session["user"] = user;
                ViewBag.returnUrl = returnUrl;
                return RedirectToAction("Index", "Home");
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