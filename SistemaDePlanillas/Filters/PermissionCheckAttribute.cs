using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SistemaDePlanillas.Filters
{
    public class PermissionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var requestValues = actionContext.RequestContext.RouteData.Values;
            string group = requestValues["group"].ToString();
            string action = requestValues["operation"].ToString();
            if (false)//resolver permisos
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            else {
                base.OnActionExecuting(actionContext);
            }
        }
    }
}