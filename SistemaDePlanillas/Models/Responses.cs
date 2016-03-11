using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models
{
    public class Responses
    {
        public static readonly string OK= "{\"status\":\"OK\"}";
        private static JavaScriptSerializer js = new JavaScriptSerializer();

        public static string Error(long errorCode)
        {
            return "{\"status\":\"ERROR\", \"error\":" + errorCode+ ", \"detail\":\"" +
                Errors.Instance.getDetail(errorCode)+"\"}";
        }

        public static string ExceptionError(Exception e)
        {
            return "{\"status\":\"ERROR\", \"error\": -1, \"detail\":\"" + e.Message+ "\"}";
        }

        public static string Simple(long status)
        {
            return status == 0 ? OK : Error(status);
        }

        public static string Error(long errorCode, string detail)
        {
            return "{\"status\":\"ERROR\", \"error\":"+errorCode+", \"detail\":\"" + detail + "\"}";
        }

        public static string WithData(object data)
        {
            return "{\"status\":\"OK\", \"data\":"+js.Serialize(data)+"}";
        }

        public static string SimpleWithData(long status, object data)
        {
            return status == 0 ? WithData(data) : Error(status);
        }

    }

    public class Errors
    {
        private Dictionary<long, string> details;

        private static readonly Errors instance = new Errors();

        private Errors()
        {
            details = new Dictionary<long, string>();
            //leer errores de la base de datos
            //insertarlos
        }
        
        public static Errors Instance
        {
            get
            {
                return instance;
            }
        }

        public string getDetail(long errorCode)
        {
            return details.ContainsKey(errorCode) ? details[errorCode] : "Error desconocido";
        }
    }
}