using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        public static string AddFixedDebit(User user, int employee, string detail, int amount)
        {

            var response = DBManager.getInstance().addDebit(employee, detail, amount);
            if (response.status == 0)
            {
                return "{status:'OK'}";
            }
            else
            {
                return "{status:'ERROR', error:43141, detail:'Paso algo grave :('}";
            }
        }
    }
}