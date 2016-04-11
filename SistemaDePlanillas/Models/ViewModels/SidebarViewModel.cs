using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.ViewModels
{
    public class SidebarViewModel : BaseViewModel
    {
        public ApplicationSubMenu[] ApplicationMenus { get; set; }
        public string ApplicationName { get; set; }
    }

    public class ApplicationSubMenu
    {
        public ApplicationSubMenu(string displayName, string glyphicon, ApplicationLink[] links)
        {
            DisplayName = displayName;
            Glyphicon = glyphicon;
            Links = links;
        }

        public string DisplayName { get; }
        public string Glyphicon { get; }
        public ApplicationLink[] Links { get; }

        public int NumberOfLinks()
        {
            return Links.Length;
        }
    }

    public class ApplicationLink
    {
        public ApplicationLink(string displayName, string action, string controller)
        {
            DisplayName = displayName;
            Action = action;
            Controller = controller;
        }
        public string DisplayName { get; }
        public string Action { get; }
        public string Controller { get; }
    }

}