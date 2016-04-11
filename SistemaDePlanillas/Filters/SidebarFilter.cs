using SistemaDePlanillas.Models;
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
        HttpContext ctx = HttpContext.Current;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string ApplicationName = "Sistema de planillas";
            dynamic ViewBag = filterContext.Controller.ViewBag;

            ViewBag.AppTitle = ApplicationName;
            NavigationMenuModel data = new NavigationMenuModel();
            SidebarViewModel viewModel = new SidebarViewModel();

            viewModel.ApplicationName = ApplicationName;
            viewModel.ApplicationMenus = data.GetData();
            ViewBag.SidebarViewModel = viewModel;

            ViewResult v = filterContext.Result as ViewResult;
            if(v != null)
            {
                BaseViewModel model = v.Model as BaseViewModel;
                if(model != null)
                {
                    //ctx.Session
                }
            }
        }
    }
}