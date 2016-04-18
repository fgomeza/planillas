using System.Web;
using System.Web.Mvc;
using SistemaDePlanillas.Filters;

namespace SistemaDePlanillas
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
