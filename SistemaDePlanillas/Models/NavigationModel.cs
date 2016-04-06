using SistemaDePlanillas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models
{
    public class NavigationMenuModel
    {
        public ApplicationSubMenu[] ApplicationMenus { get; private set; }

        public ApplicationSubMenu[] GetData()
        {
            ApplicationMenus = new ApplicationSubMenu[]
            {
                new ApplicationSubMenu("Home", null, new ApplicationLink[] {
                    new ApplicationLink("Super Testing", "Test", "Home"),
                    new ApplicationLink("Dashboard template", "Dashboard", "Home")
                }),
                new ApplicationSubMenu("Users", null, new ApplicationLink[] {
                    new ApplicationLink("Lista de Usuario", "Index", "Users"),
                    new ApplicationLink("Crear Usuario", "Create", "Users")
                }),
                new ApplicationSubMenu("Manage", null, new ApplicationLink[] {
                    new ApplicationLink("Subir un archivo", "UploadFile", "Manage")
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

            return ApplicationMenus;
        }
    }

}