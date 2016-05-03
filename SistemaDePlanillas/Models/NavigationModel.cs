using SistemaDePlanillas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models
{
    public class NavigationMenuModel
    {
        public ApplicationSubMenu[] ApplicationMenus {
            get
            {
                return new ApplicationSubMenu[]
                {
                    new ApplicationSubMenu("Home", null, new ApplicationLink[] {
                        new ApplicationLink("Super Testing", "test"),
                        new ApplicationLink("Dashboard template", "dashboard")
                    }),
                    new ApplicationSubMenu("Planillas", null, new ApplicationLink[] {
                        new ApplicationLink("Subir un archivo", "uploadfile"),
                        new ApplicationLink("Cálculo de planillas", "payroll")
                    }),
                    new ApplicationSubMenu("Administración", null, new ApplicationLink[] {
                        new ApplicationLink("Administrar usuarios", "users"),
                        new ApplicationLink("Administrar asociados", "employees"),
                        new ApplicationLink("Administrar deducciones", "debits"),
                        new ApplicationLink("Administrar roles", "roles")
                    })
                    /*
                    new ApplicationSubMenu("Settings", null, new ApplicationLink[] {
                        new ApplicationLink("Home", "#", null),
                        new ApplicationLink("Messages", "#", null),
                        new ApplicationLink("Options", "#", null),
                        new ApplicationLink("Shoutbox", "#", null),
                        new ApplicationLink("Staff List", "#", null),
                        new ApplicationLink("Transactions", "#", null),
                        new ApplicationLink("Rules", "#", null),
                        new ApplicationLink("Logout", "#", null)
                    })
                    */
                };
            }
        }
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
        public ApplicationLink(string displayName, string page)
        {
            DisplayName = displayName;
            Page = page;
        }
        public string DisplayName { get; }
        public string Page { get; }
    }


}