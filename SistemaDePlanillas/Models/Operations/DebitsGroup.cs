using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        public static string AddFixedDebit(User user, long employee, string detail, long amount)
        {

            var dataBaseResponse = DBManager.getInstance().addDebit((int)employee, detail, amount);
            return Responses.Simple(dataBaseResponse.status);

        }

        public static string test(User user)
        {

            DBManager.getInstance().addExtra(5, "6565", 656.75f);
            return "fasfsa";
        }

    }
}