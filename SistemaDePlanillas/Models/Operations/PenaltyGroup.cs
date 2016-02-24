using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    /// <summary>
    /// Modulo de mantenimiento de penalizaciones
    /// </summary>
    public class PenaltyGroup
    {
        /// <summary>
        /// Genera una penalizacion y la asocia a un empleado
        /// </summary>
        /// <param name="employeeId">Identificador del empleado</param>
        /// <param name="detail">Motivo de la penalizacion</param>
        /// <param name="amount">Monto a descontar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
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
        /// <summary>
        /// Elimina una penalizacion asociada a un empleado
        /// </summary>
        /// <param name="penaltyId">Identificador de la penalizacion</param>
        /// <returns>>Estado de la transaccion en formato JSON</returns>
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
        /// <summary>
        /// Actualiza la informacion de una penalizacion
        /// </summary>
        /// <param name="penaltyId">Identificador de la penalizacion</param>
        /// <param name="detail">Motivo de la penalizacion</param>
        /// <param name="amount">Monto a descontar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
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
        /// <summary>
        /// Accesa a las penalizaciones vinculadas a un empleado
        /// </summary>
        /// <param name="employeeId">Identificador del empleado</param>
        /// <returns>
        /// Lista con las penalizaciones asociadas al usuario
        /// Estado de la transaccion en formato JSON
        /// </returns>
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
        /// <summary>
        /// Accesa a una penalizacion por su identificador
        /// </summary>
        /// <param name="extraId">Identificador de la penalizacion</param>
        /// <returns>
        /// Penalizacion asociada al identificador
        /// Estado de la transaccion en formato JSON
        /// </returns>
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