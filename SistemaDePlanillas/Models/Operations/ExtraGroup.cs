using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
namespace SistemaDePlanillas.Models.Operations
{
    public class ExtraGroup
    {
        public string AddExtras(long employeeId, string detail,float amount)
        {
            try
            {
                DBManager.getInstance().addExtra((int)employeeId, detail, amount);
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string RemoveExtras(long extraId)
        {
            try
            {
                DBManager.getInstance().deleteExtra((int)extraId);
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string UpdateExtras(long extraId, string detail, float amount)
        {
            try
            {
                DBManager.getInstance().updateExtra((int)extraId, detail, amount);
                return "";
            }
            catch (Exception)
            {

                return "";
            }
            
        }
        public string ListAllExtras(long employeeId)
        {
            try
            {
                List<Extra> allExtras = DBManager.getInstance().selectExtras((int)employeeId).detail;
                return MessageManager.Serializer.Serialize(allExtras);
            }
            catch (Exception)
            {

                return "";
            }
      
        }
        public string FindExtraById(long extraId)
        {
            try
            {
                Extra extra = DBManager.getInstance().selectExtra((int)extraId).detail;
                return MessageManager.Serializer.Serialize(extra);
            }
            catch (Exception)
            {
                return "";
                
            }
        }
    }
}