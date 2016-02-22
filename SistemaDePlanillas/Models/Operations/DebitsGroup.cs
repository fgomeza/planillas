using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
<<<<<<< HEAD
        public static string AddFixedDebit(User user, int employee, string detail, int amount)
        {

            var response = DBManager.getInstance().addDebit(employee, detail, amount);
            if (response.status == 0)
=======
        public static string add1(User user, long employee, string detail, long amount)
        {

            var res = DBManager.getInstance().addDebit((int)employee, detail, amount);
            if (res.status == 0)
>>>>>>> refs/remotes/origin/master
            {
                return "{status:'OK'}";
            }
            else
            {
                return "{status:'ERROR', error:43141, detail:'Paso algo grave :('}";
            }
        }

        public static string test(User user, string texto, long numero, double dec)
        {
            return "Recibido: "+user.name+","+texto+"," + numero+"," + dec;
        }

    }
}