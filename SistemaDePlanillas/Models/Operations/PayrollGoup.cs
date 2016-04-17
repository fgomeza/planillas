using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{

    public class PayrollGroup
    {


        public static Response calculate(User user, DateTime endDate)
        {
            List<object> rows= new List<object>();
            var employees = DBManager.Instance.selectAllEmployees(user.Location).Detail;
            foreach (var employee in employees)
            {
                var calls = DBManager.Instance.callListByEmployee(employee.id,endDate).Detail;
                long totalCalls = calls.Sum(c => c.calls);
                var penalties = penaltiesByEmployee(employee, endDate);

                rows.Add(new
                {
                    employee = employee.name,
                    call = new { amount=totalCalls, total = totalCalls,callList=calls},
                    penalties =penalties
                });
            }
            

        }


        private static  List<object> penaltiesByEmployee(Employee employee, DateTime endDate)
        {
            var penaltiesByEmployee = DBManager.Instance.selectAllPenalty(employee.id, endDate).Detail;
            Dictionary<string, List<Penalty>> penaltiesByType = new Dictionary<string, List<Penalty>>();
            foreach (var penalty in penaltiesByEmployee)
            {
                if (penaltiesByType.ContainsKey(penalty.typeName))
                    penaltiesByType[penalty.typeName].Add(penalty);
                else
                    penaltiesByType.Add(penalty.typeName, new List<Penalty>() { penalty });
            }
            List<object> penalties = new List<object>();
            foreach (var penalty in penaltiesByType)
            {
                penalties.Add(new { typeName = penalty.Key, amount = penalty.Value.Sum(p => p.amount), total = penalty.Value.Sum(p => p.amount * p.penaltyPrice), typeList = penalty.Value });
            }
            return penalties;
        }
    }

}