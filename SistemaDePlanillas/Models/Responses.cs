using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models
{
    public class Responses
    {
        public static string Ok = "OK";
        public static Response OK = new Response() { status = "OK" };
        private static JavaScriptSerializer js = new JavaScriptSerializer();


        public static Response ExceptionError(Exception e)
        {
            return new ErrorResponse() { status = "ERROR", error = -1, detail = e.InnerException.Message!=null?e.InnerException.Message:e.Message };
        }


        public static Response Error(long errorCode, string detail)
        {
            return new ErrorResponse() { status = "ERROR", error = errorCode, detail = detail };
        }
        public static Response AplicationError(string errorCode, string detail)
        {
            return new AplicationErrorResponse() { status = "ERROR", error = errorCode, detail = detail };
        }

        public static Response WithData(object data)
        {
            return new DataResponse() { status = "OK", data = data };
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

    public class AplicationErrorResponse : Response
    {
        public string error;
        public string detail;
    }

    public class DataResponse : Response
    {
        public object data;
    }

}