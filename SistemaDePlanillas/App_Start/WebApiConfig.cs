using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SistemaDePlanillas.Filters;
using System.Web.Http.Routing;
using System.Net.Http;

namespace SistemaDePlanillas
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Filters.Add(new PermissionCheckAttribute());
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ActionsApi",
                routeTemplate: "api/Action/{action}/{group}/{operation}/{call}",
                defaults: new { controller = "Actions", call = RouteParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post)}
            );
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}