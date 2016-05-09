using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace SistemaDePlanillas.Models
{
    public class Result<T>
    {
        public long Status = 0;
        public T Detail;
    }

    public class Employee
    {
        public long id;
        public string idCard;
        public string name;
        public long location;
        public string account;
        public bool cms;
        public string cmsText;
        public long calls;
        public double salary;
        public bool active;
        public double negativeAmount;
    }

    public class Call
    {
        public long employee;
        public long calls;
        public Nullable<DateTime> date;
        public Nullable<TimeSpan> hours;
    }

    public class Payroll
    {
        public long id;
        public DateTime endDate;
        public long user;
        public string json;
    }

    public class DebitType
    {
        public long id;
        public string name;
        public Nullable<double> interestRate;
        public Nullable<long> months;
        public long location;
        public bool payment;
    }

    public class Debit
    {
        public long id;
        public long employee;
        public string detail;
        public double amount;
        public long type;
        public string typeName;
    }

    public class PaymentDebit
    {
        public long id;
        public long employee;
        public string detail;
        public DateTime initialDate;
        public double total;
        public double interestRate;
        public long paymentsMade;
        public long missingPayments;
        public double remainingAmount;
        public long type;
        public string typeName;
    }

    public class AmortizationDebit
    {
        public long id;
        public long employee;
        public string detail;
        public DateTime initialDate;
        public double total;
        public double interestRate;
        public long paymentsMade;
        public long missingPayments;
        public double remainingAmount;
        public long type;
        public string typeName;
    }

    public class Extra
    {
        public long id;
        public long employee;
        public string detail;
        public long hours;
    }

    public class Penalty
    {
        public long id;
        public long employee;
        public DateTime date;
        public string detail;
        public bool active;
        public double amount;
        public long type;
        public string typeName;
        public double penaltyPrice;
    }

    public class PenaltyType
    {
        public long id;
        public string name;
        public double price;
    }


    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long Role { get; set; }
        public long Location { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        //public HttpSessionStateBase session { get; set; }
    }

    public class Location
    {
        public long Id;
        public string Name;
        public double CallPrice;
        public double Capitalization=0.04;
        public Nullable<long> LastPayroll;
        public Nullable<long> CurrentPayroll;
        public bool Active;
        public bool isPendingToApprove;
    }

    public class Role
    {
        public long id;
        public string name;
        public long location;
        public bool active;
        public Dictionary<string, Dictionary<string,bool>> privileges;

        public Role(long id, string name, long location, bool active, List<Tuple<string,string,bool>> privs)
        {
            this.id = id;
            this.name = name;
            this.location = location;
            this.active = active;
            this.privileges = 
                privs.GroupBy(p => p.Item1, p => p, (k, l) => new { key = k, list = l }).ToDictionary(g => g.key, g => g.list.ToDictionary(o => o.Item2, o => o.Item3));
        }

    }

    public class OperationsGroup
    {
        public readonly int id;
        public readonly string desc;
        public readonly string name;
        public readonly string icon;
        public readonly List<Operation> operations;

        public OperationsGroup(string desc, string name, string icon, List<Operation> operations)
        {
            this.desc = desc;
            this.name = name;
            this.icon = "".Equals(icon) ? "" : "glyphicon glyphicon-" + icon;
            this.operations = operations;
        }
    }

    public class Operation
    {
        public string Id;
        public  string Name;
        public  string Description;
        public bool isPayrollCalculationRelated;
        public string Group;

        public Operation(string id, string operationName, string groupName, string desc, bool isPayrollCalculationRelated)
        {
            Id = id;
            Name = operationName;
            Description = desc;
            Group = groupName;
            this.isPayrollCalculationRelated = isPayrollCalculationRelated;
        }
    }
}