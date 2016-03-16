using SistemaDePlanillas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDePlanillas.Filters
{
    public class SidebarFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult v = filterContext.Result as ViewResult;
            if(v != null)
            {
                BaseViewModel model = v.Model as BaseViewModel;
                if(model != null)
                {
                    model.SidebarViewModel = new SidebarViewModel();
                }
            }
        }
    }
}