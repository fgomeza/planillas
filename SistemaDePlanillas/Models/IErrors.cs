using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using SistemaDePlanillas.Models.App_LocalResoures;

namespace SistemaDePlanillas.Models
{
    public class IErrors
    {

       // private static ResourceManager Errors = new ResourceManager(Errors.BaseName, Assembly.GetExecutingAssembly());
       // private static ResourceManager DescriptionErrors = new ResourceManager(DescriptionError.ResourceManager., Assembly.GetExecutingAssembly());

        public static AppException validateException(Exception e)
        {
            if (e.InnerException != null)
            {
                e = e.InnerException;
                if (e.InnerException != null && e.InnerException is NpgsqlException)
                {
                    string code = App_LocalResoures.Errors.ResourceManager.GetString((e.InnerException as NpgsqlException).ConstraintName);
                    string description = App_LocalResoures.DescriptionError.ResourceManager.GetString(code);
                    throw new AppException(code, description);
                }
            }
            throw e;
        }

        public static AppException validateException(string code)
        {

            string description = App_LocalResoures.DescriptionError.ResourceManager.GetString(code);
            throw new AppException(code, description);
        }

        public static string inexistentEmployee = "EmployeeError1";
        public static string employeeInactive = "EmployeeError4";
        public static string inexistentLocation = "LocationError1";
        public static string locationInactive = "LocationError2";
        public static string inexistentPenalty = "PenaltyError1";
        public static string penaltyInactive = "PenaltyError2";
        public static string inexistentRole = "RoleError1";
        public static string roleInactive = "RoleError4";
        public static string inexistentUser = "UserError1";
        public static string userInactive = "UserError4";
        public static string inexistentDebit = "DebitError1";
        public static string inexistentExtra = "ExtraError1";
        public static string inexistentGroup = "GroupError1";
        
    }

    public class AppException : Exception
    {
        public  string Code { get; set; }
        public  string DescriptionError { get; set; }

        public AppException(string code, string description) : base(description)
        {
            Code = code;
            DescriptionError = description;
        }
    }

}
