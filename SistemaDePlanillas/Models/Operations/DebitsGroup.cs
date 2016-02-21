using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        static string add1(User user, int employee, string detail, int amount)
        {
            var res = DBManager.getInstance().addDebit(employee, detail, amount);
            if (res.status == 0)
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