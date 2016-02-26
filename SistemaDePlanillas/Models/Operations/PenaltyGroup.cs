using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models.Operations
{
    /// <summary>
    /// Modulo de mantenimiento de penalizaciones
    /// </summary>
    public class RecessGroup
    {
        /// <summary>
        /// Genera una penalizacion y la asocia a un empleado
        /// </summary>
        /// <param name="employeeId">Identificador del empleado</param>
        /// <param name="detail">Motivo de la penalizacion</param>
        /// <param name="amount">Monto a descontar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
        public static string add(User user,long employeeId, string detail, float amount,long months)
        {
            try
            {
                var result = DBManager.getInstance().addRecess(employeeId, detail, amount,months).status;
                return Responses.Simple(result);
            }
            catch (Exception ex)
            {
                return Responses.ExceptionError(ex);
            }
        }
        /// <summary>
        /// Elimina una penalizacion asociada a un empleado
        /// </summary>
        /// <param name="recessId">Identificador de la penalizacion</param>
        /// <returns>>Estado de la transaccion en formato JSON</returns>
        public static string remove(User user,long recessId)
        {
            try
            {
                var result = DBManager.getInstance().deleteRecess(recessId).status;
                return Responses.Simple(result);
            }
            catch (Exception ex)
            {
                return Responses.ExceptionError(ex);
            }
        }
        /// <summary>
        /// Actualiza la informacion de una penalizacion
        /// </summary>
        /// <param name="penaltyId">Identificador de la penalizacion</param>
        /// <param name="detail">Motivo de la penalizacion</param>
        /// <param name="amount">Monto a descontar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
        public static string modify(User user,long penaltyId, string detail, float amount,long months,double remainingdebt)
        {
            try
            {
                var result =  DBManager.getInstance().updateRecess(penaltyId, detail, amount, months, remainingdebt);
                return Responses.SimpleWithData(result.status, result.detail);
            }
            catch (Exception ex)
            {

                return Responses.ExceptionError(ex);
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
        public static string get_all(User user,long employeeId)
        {
            try
            {
                var result = DBManager.getInstance().selectAllRecess(employeeId);
                return Responses.SimpleWithData(result.status, result.detail);
            }
            catch (Exception ex)
            {

                return Responses.ExceptionError(ex);
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
        public static string get(User user,long recessId)
        {
            try
            {
                var result = DBManager.getInstance().selectRecess(recessId);
                return Responses.SimpleWithData(result.status, result.detail);
            }
            catch (Exception ex)
            {
                return Responses.ExceptionError(ex);

            }
        }
    }
}