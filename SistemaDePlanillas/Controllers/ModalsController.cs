using SistemaDePlanillas.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Controllers
{
    [Authorize]
    [AjaxOnly]
    public class ModalsController : Controller
    {
        public ActionResult CreateUser()
        {
            return PartialView();
        }

        public ActionResult Template()
        {
            return PartialView();
        }
    }
}