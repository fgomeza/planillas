using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Filters
{
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        private bool IsValidForRequest(ControllerContext controllerContext)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(!IsValidForRequest(filterContext))
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }

    }
}