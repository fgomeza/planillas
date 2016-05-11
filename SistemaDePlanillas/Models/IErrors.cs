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

        public static void validateException(Exception e)
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

        public static void validateException(string code)
        {
            string description = App_LocalResoures.DescriptionError.ResourceManager.GetString(code);
            throw new AppException(code, description);
        }

        
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
