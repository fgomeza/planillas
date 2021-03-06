﻿using SistemaDePlanillas.Models;
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
            User user;
            AppException error;
            var ok= SessionManager.Instance.validateUser(model.Username, model.Password,out user,out error);
            if (ModelState.IsValid && ok)
            {
                SessionManager.Instance.setSessionUser(Response, user, model.RememberMe);
                ViewBag.returnUrl = returnUrl;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("LoginError", error.DescriptionError);
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
            SessionManager.Instance.removeSessionUser(Request);
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