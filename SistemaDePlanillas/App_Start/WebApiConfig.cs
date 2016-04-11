using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SistemaDePlanillas
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
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