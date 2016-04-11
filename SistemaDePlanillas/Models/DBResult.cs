using System;
using System.Collections.Generic;
using System.Web;

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

    }

    public class Payroll
    {
        public long id;
        public NpgsqlTypes.NpgsqlDate date;
        public long user;
        public string file;
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
    }

    public class PaymentDebit
    {
        public long id;
        public long employee;
        public string detail;
        public DateTime initialDate;
        public double total;
        public Nullable<double> interestRate;
        public Nullable<long> paymentsMade;
        public Nullable<long> missingPayments;
        public double remainingAmount;
        public double payment;
        public long type;
    }
    public class Extra
    {
        public long id;
        public long employee;
        public string detail;
        public double amount;
    }

    public class Penalty
    {
        public long id;
        public long employee;
        public string detail;
        public Nullable<double> amount;
        /* public long paymentsMade;
         public long missingPayments;
         public double remainingRecess;
         public double payment;*/
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
        //public HttpSessionStateBase session { get; set; }
    }

    public class Location
    {
        public long Id;
        public string Name;
        public double CallPrice;
        public long LastPayroll;
        public long CurrentPayroll;
    }

    public class Role
    {
        //public NavbarConfig navbar;
        public long id;
        public string name;
        public long location;
        public Dictionary<string, HashSet<string>> privileges;

        public Role(long id, string name, long location, List<Tuple<string, string>> privs)
        {
            this.id = id;
            this.name = name;
            this.location = location;
            privileges = new Dictionary<string, HashSet<string>>();
            foreach (var priv in privs)
            {
                if (!privileges.ContainsKey(priv.Item1))
                {
                    privileges.Add(priv.Item1, new HashSet<string>());
                }
                privileges[priv.Item1].Add(priv.Item2);
            }
            //navbar = new NavbarConfig(privileges);
        }

        public void update()
        {
          /*  var privs = DBManager.Instance.selectRolePrivileges(id).detail;
            privileges = new Dictionary<string, HashSet<string>>();
            foreach (var priv in privs)
            {
                if (!privileges.ContainsKey(priv.Item1))
                {
                    privileges.Add(priv.Item1, new HashSet<string>());
                }
                privileges[priv.Item1].Add(priv.Item2);
            }
            //navbar = new NavbarConfig(privileges);*/
        }
    }

    public class OperationsGroup
    {
        public readonly int id;
        public readonly string desc;
        public readonly string name;
        public readonly string icon;
        public readonly Dictionary<string, Operation> operations;

        public OperationsGroup(string desc, string name, string icon, List<Operation> operations)
        {
            this.desc = desc;
            this.name = name;
            this.icon = "".Equals(icon) ? "" : "glyphicon glyphicon-" + icon;
            this.operations = new Dictionary<string, Operation>();
            foreach (Operation op in operations)
            {
                this.operations[op.name] = op;
            }
        }
    }

    public class Operation
    {
        public readonly string desc;
        public readonly string url;
        public readonly string name;

        public Operation(string desc, string operationName, string groupName)
        {
            this.desc = desc;
            name = operationName;
            url = "/action/" + groupName + "/" + operationName;
        }
    }
}