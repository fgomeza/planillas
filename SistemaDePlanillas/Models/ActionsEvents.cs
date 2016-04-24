using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SistemaDePlanillas.Models
{
    public class ActionsEvents : IActionsEventsListener
    {
        public void OnActionComplete(Response response, User user, DateTime callTime, string group, string operation, IDictionary<string, object> args)
        {
            Logger.Instance.LogAction(response, group, operation, args, user, callTime);
        }

        public void OnActionError(Response response, User user, DateTime callTime, string group, string operation, IDictionary<string, object> args, string errorCode, string description)
        {
            Logger.Instance.LogAction(response, group, operation, args, user, callTime);
        }

        public void OnBadActionArguments(User user, DateTime callTime, string group, string operation, IDictionary<string, object> args, IEnumerable<ParameterInfo> expected)
        {

        }

        public void OnException(User user, DateTime callTime, string group, string operation, IDictionary<string, object> args, Exception e)
        {
            Logger.Instance.LogActionException(e, group, operation, args, user, callTime);
        }
    }
}