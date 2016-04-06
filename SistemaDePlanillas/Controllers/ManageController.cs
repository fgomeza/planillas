using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult UploadFile()
        {
            ViewBag.IsAjaxRequest = Request.IsAjaxRequest();
            return View();
        }
    }
}