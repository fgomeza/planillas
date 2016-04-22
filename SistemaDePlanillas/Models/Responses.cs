using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models
{
    public class Responses
    {
        private static Response OK = new Response() { status = "OK" };
        private static JavaScriptSerializer js = new JavaScriptSerializer();

        public static Response Error(long errorCode)
        {
            return new ErrorResponse() {status="ERROR",error=errorCode, detail=Errors.Instance.getDetail(errorCode)};
        }

        public static Response ExceptionError(Exception e)
        {
            return new ErrorResponse() { status = "ERROR", error = -1, detail = e.InnerException.Message!=null?e.InnerException.Message:e.Message };
        }

        public static Response Simple(long status)
        {
            return status == 0 ? OK : Error(status);
        }

        public static Response Error(long errorCode, string detail)
        {
            return new ErrorResponse() { status = "ERROR", error = errorCode, detail = detail };
        }

        public static Response WithData(object data)
        {
            return new DataResponse() { status = "OK", data = data };
        }

        public static Response SimpleWithData(long status, object data)
        {
            return status == 0 ? WithData(data) : Error(status);
        }

    }

    public class Response {
        public string status;
    };

    public class ErrorResponse : Response
    {
        public long error;
        public string detail;
    }

    public class DataResponse : Response
    {
        public object data;
    }

    public class Errors
    {
        private Dictionary<long, string> details;

        private static Errors instance;

        private Errors()
        {
            details = new Dictionary<long, string>();
            /*
            var err = DBManager.Instance.selectAllErrors().Detail;
            foreach (var x in err)
                details.Add(x.Item1,x.Item2);
                */
            details[1] = "inexistentEmployee";
            details[2] = "inexistentExtra";
            details[3] = "inexistentUser";
            details[4] = "inexistentDebit";
            details[5] = "inexistentLocation";
            details[6] = "inexistentRole";
            details[7] = "inexistentOperation";
            details[8] = "inexistentPenalty";
            details[9] = "inexistentGroup";
            details[10] = "employeeInactive";
            details[11] = "locationInactive";
            details[12] = "penaltyInactive";
            details[13] = "roleInactive";
            details[14] = "userInactive";
        }
        
        public static Errors Instance
        {
            get
            {
                return instance == null ? (instance = new Errors()) : instance;
            }
        }

        public string getDetail(long errorCode)
        {
            return details.ContainsKey(errorCode) ? details[errorCode] : "Error desconocido";
        }
    }
}