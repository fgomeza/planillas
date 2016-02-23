using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models
{
    public class Responses
    {
        public static readonly string OK= "{status:'OK'}";

        public static string Error(int errorCode)
        {
            return "{status:'ERROR', error:"+errorCode+", detail:'"+
                Errors.getInstance().getDetail(errorCode)+"'}";
        }

        public static string ExceptionError(Exception e)
        {
            return "{status:'ERROR', error: -1, detail:'" +e.Message+ "'}";
        }

        public static string Simple(int status)
        {
            return status == 0 ? OK : Error(status);
        }

    }

    public class Errors
    {
        private Dictionary<int, string> detalles;

        private static Errors instance;
        
        public static Errors getInstance()
        {
            return instance != null ? instance : (instance = new Errors());
        }

        public string getDetail(int errorCode)
        {
            //return detalles[errorCode];
            return "detalle del error (por implementar)";
        }
    }
}