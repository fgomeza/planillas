﻿using SistemaDePlanillas.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Controllers
{
    [AjaxOnly]
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult UploadFile()
        {
            return PartialView();
        }
    }
}