using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;
using SistemaDePlanillas.Models;

namespace SistemaDePlanillas.Filters
{
    public class PermissionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var requestValues = actionContext.RequestContext.RouteData.Values;
            string group = requestValues["group"].ToString();
            string action = requestValues["operation"].ToString();

            User user = SessionManager.Instance.getSessionUser(actionContext.RequestContext);
            
            if (SessionManager.Instance.verifyOperation(user, group, action))
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}