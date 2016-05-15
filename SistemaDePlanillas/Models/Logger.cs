using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models
{
    public class Logger
    {
        private static Logger instance;

        public static Logger Instance
        {
            get
            {
                return instance == null ? (instance = new Logger()) : instance;
            }
        }

        public void LogAction(Response response, string group, string action, IDictionary<string, object> args, User user, DateTime callTime)
        {

        }

        public void LogActionException(Exception e, string group, string action, IDictionary<string, object> args, User user, DateTime callTime)
        {

        }
    }
}