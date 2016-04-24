using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SistemaDePlanillas.Models
{
    public interface IActionsEventsListener
    {
        void OnActionComplete(Response response, User user, DateTime callTime, string group, string operation, IDictionary<string, object> args);
        void OnActionError(Response response, User user, DateTime callTime, string group, string operation, IDictionary<string, object> args, string errorCode, string description);
        void OnBadActionArguments(User user, DateTime callTime, string group, string operation, IDictionary<string, object> args, IEnumerable<ParameterInfo> expected);
        void OnException(User user, DateTime callTime, string group, string operation, IDictionary<string, object> args, Exception e);
    }
}