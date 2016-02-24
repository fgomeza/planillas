using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    public class PenaltyGroup
    {
        public string AddPenalty(long employeeId, string detail, float amount)
        {
            try
            {
                //DBManager.getInstance().addPenalty(employeeId, detail, amount);
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string RemoveExtras(long penaltyId)
        {
            try
            {
                //DBManager.getInstance().deletePenalty((int)penaltyId);
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string UpdateExtras(long penaltyId, string detail, float amount)
        {
            try
            {
                //DBManager.getInstance().updatePenalty((int)penaltyId, detail, amount);
                return "";
            }
            catch (Exception)
            {

                return "";
            }

        }
        public string ListAllPenalties(long employeeId)
        {
            try
            {
                //List<Extra> allExtras = DBManager.getInstance().selectPenalties((int)employeeId).detail;
                //return MessageManager.Serializer.Serialize(allExtras);
                return "";
            }
            catch (Exception)
            {

                return "";
            }

        }
        public string FindPenaltyById(long extraId)
        {
            try
            {
                //Extra extra = DBManager.getInstance().selectPenalty((int)extraId).detail;
                //return MessageManager.Serializer.Serialize(extra);
                return "";
            }
            catch (Exception)
            {
                return "";

            }
        }
    }
}