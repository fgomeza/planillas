using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        public static string AddFixedDebit(User user, int employee, string detail, int amount)
        {

            var dataBaseResponse = DBManager.getInstance().addDebit(employee, detail, amount);
            return new JavaScriptSerializer().Serialize(dataBaseResponse);
   
        }

        public static string test(User user, string texto, long numero, double dec)
        {
            return "Recibido: "+user.name+","+texto+"," + numero+"," + dec;
        }

    }
}