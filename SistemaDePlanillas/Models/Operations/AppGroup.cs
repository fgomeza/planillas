using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class AppGroup
    {
        public static Response actions(User user)
        {
            string @namespace = "SistemaDePlanillas.Models.Operations";

            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == @namespace && t.Name.EndsWith("Group")
                    select t;

            var groups = q.ToList();
            List<object> actions = new List<object>();
            foreach (var group in groups)
            {
                var methods = group.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase).Where(m => m.GetParameters().Length > 0 && m.GetParameters()[0].ParameterType.Equals(typeof(User)))
                    .Select((m) => m.Name.Replace('_', '/') + getParametersDesc(m.GetParameters()));
                actions.Add(new { groupName = group.Name.Substring(0,group.Name.Length-5), actions = methods });
            }
            return Responses.WithData(actions);
        }

        public static Response actions(User user, string group)
        {
            string @namespace = "SistemaDePlanillas.Models.Operations";
            Type type = Type.GetType(@namespace+"."+group+"Group", false, true);
            if (type == null)
            {
                return Responses.Error(123, "No se encuentra el grupo");
            }
            var methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase).Where(m => m.GetParameters().Length > 0 && m.GetParameters()[0].ParameterType.Equals(typeof(User)))
                .Select((m) => m.Name.Replace('_', '/') + getParametersDesc(m.GetParameters()));
            return Responses.WithData(methods);
        }

        private static string getParametersDesc(ParameterInfo[] parameters)
        {
            if (parameters.Length <= 1)
            {
                return "";
            }
            string p = ": ";
            for (int i = 1; i < parameters.Length; i++)
            {
                Type t = parameters[i].ParameterType;
                string name;
                if (t.IsAssignableFrom(typeof(IEnumerable<object>)))
                {
                    name = "Array";
                }
                else if (t.Equals(typeof(Int64))){
                    name = "Int";
                }
                else
                {
                    name = t.Name;
                }
                p += parameters[i].Name+"("+name+"), ";
            }
            return p.Substring(0,p.Length-2);
        }
    }
}