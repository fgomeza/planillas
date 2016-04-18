using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SistemaDePlanillas.Filters;

namespace SistemaDePlanillas
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new PermissionCheckAttribute());
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "OperationsApi",
                routeTemplate: "api/Action/{group}/{operation}/{call}",
                defaults: new { controller = "OperationsApi", call = RouteParameter.Optional }
            );
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}