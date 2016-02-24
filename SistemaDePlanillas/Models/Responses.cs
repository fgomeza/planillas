﻿using System;
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

        public static string Error(int errorCode)
        {
            return "{\"status\":\"ERROR\", \"error\":" + errorCode+ ", \"detail\":\"" +
                Errors.getInstance().getDetail(errorCode)+"\"}";
        }

        public static string ExceptionError(Exception e)
        {
            return "{\"status\":\"ERROR\", \"error\": -1, \"detail\":\"" + e.Message+ "\", \"stackTrace\": \"" + e.StackTrace+ "\"}";
        }

        public static string Simple(int status)
        {
            return status == 0 ? OK : Error(status);
        }

        public static string Error(int errorCode, string detail)
        {
            return "{\"status\":\"ERROR\", \"error\":"+errorCode+", \"detail\":\"" + detail + "\"}";
        }

        public static string WithData(object data)
        {
            return "{\"status\":\"OK\", \"data\":"+js.Serialize(data)+"}";
        }

        public static string SimpleWithData(int status, object data)
        {
            return status == 0 ? WithData(data) : Error(status);
        }

    }

    public class Errors
    {
        private Dictionary<int, string> details;

        private static Errors instance;

        private Errors()
        {
            details = new Dictionary<int, string>();
            //leer errores de la base de datos
            //insertarlos
        }
        
        public static Errors getInstance()
        {
            return instance != null ? instance : (instance = new Errors());
        }

        public string getDetail(int errorCode)
        {
            return details.ContainsKey(errorCode) ? details[errorCode] : "Error desconocido";
        }
    }
}