using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        static string add1()
        {
            var res=DBManager.getInstance().addExtra(1, "detalle", 42525252);
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