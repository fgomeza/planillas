﻿using SistemaDePlanillas.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Controllers
{
    [Authorize]
    [AjaxOnly]
    public class PartialViewsController : Controller
    {
        public ActionResult Index()
        {
            return Dashboard();
        }

        public ActionResult Dashboard()
        {
            return PartialView();
        }

        public ActionResult Test()
        {
            return PartialView();
        }

        public ActionResult Users()
        {
            return PartialView();
        }

        public ActionResult Employees()
        {
            return PartialView();
        }

        public ActionResult Debits()
        {
            return PartialView();
        }

        public ActionResult UploadFile()
        {
            return PartialView();
        }

        public ActionResult Roles()
        {
            return PartialView();
        }

        public ActionResult Payroll()
        {
            return PartialView();
        }

        public ActionResult Welcome()
        {
            return PartialView();
        }

        public ActionResult Extras()
        {
            return PartialView();
        }
    }
}
