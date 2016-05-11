using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Manager
{
    public class DBManager
    {
        private static DBManager instance;

        public DebitsManager debits { get; set; }
        public EmployeeManager employees { get; set; }
        public ExtrasManager extras { get; set; }
        public LocationManager locations { get; set; }
        public OperationsManager operations { get; set; }
        public OperationsGroupManager groups { get; set; }
        public PenaltiesManager penalties { get; set; }
        public PayrollManager payrolls { get; set; }
        public RolesManager roles { get; set; }
        public SalaryManager salaries { get; set; }
        public UsersManager users { get; set; }
        public CallManager calls { get; set; }

        private DBManager()
        {
            debits = new DebitsManager();
            employees = new EmployeeManager();
            extras = new ExtrasManager();
            locations = new LocationManager();
            operations = new OperationsManager();
            groups = new OperationsGroupManager();
            penalties = new PenaltiesManager();
            payrolls = new PayrollManager();
            roles = new RolesManager();
            salaries = new SalaryManager();
            users = new UsersManager();
            calls = new CallManager();
        }

        public static DBManager Instance
        {
            get
            {
                return instance == null ? (instance = new DBManager()) : instance;
            }
        }
    }
}