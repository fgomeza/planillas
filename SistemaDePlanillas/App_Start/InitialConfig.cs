using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDePlanillas.Models;

namespace SistemaDePlanillas.App_Start
{
    public class InitialConfig
    {
        public static void init()
        {
            DBManager dbManager = DBManager.Instance;
            SessionManager sessionManager = SessionManager.Instance;
            ViewManager viewManager = ViewManager.Instance;
            Errors errores = Errors.Instance;
        }
    }
}