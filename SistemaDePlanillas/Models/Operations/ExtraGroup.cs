using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
namespace SistemaDePlanillas.Models.Operations
{
    /// <summary>
    /// Modulo para manteminiemto de horas extras
    /// </summary>
    public class ExtrasGroup
    {
        /// <summary>
        /// Genera un pago extra y lo asocia a un empleado
        /// </summary>
        /// <param name="employeeId">identificador del empleado</param>
        /// <param name="detail">Motivo del pago</param>
        /// <param name="amount">Cantidad a pagar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
        public static Response add(User user,long employeeId, string detail,double amount)
        {
                var result = DBManager.Instance.addExtra(employeeId, detail, (float)amount).Status;
                return Responses.Simple(result);
        }
        /// <summary>
        /// Elimina un pago extra
        /// </summary>
        /// <param name="extraId">identificador del pago extra</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
        public static Response remove(User user,long extraId)
        {

                var result = DBManager.Instance.deleteExtra(extraId).Status;
                return Responses.Simple(result);

        }
        /// <summary> 
        /// Actualiza la informacio de un pago extra
        /// </summary>
        /// <param name="extraId">Identificador del pago extra</param>
        /// <param name="detail">Motivo del pago</param>
        /// <param name="amount">Cantidad a pagar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
        public static Response modify(User user,long extraId, string detail, float amount)
        {

                var result = DBManager.Instance.updateExtra(extraId, detail, amount).Status;
                return Responses.Simple(result);


            
        }
        /// <summary>
        /// Accesa los pagos extra asociados a un empleado
        /// </summary>
        /// <param name="employeeId">Identificador del empleado</param>
        /// <returns>
        /// Lista con los cagos extra asociados al empleado
        /// Estado de la transaccion en formato JSON
        /// </returns>
        public static Response get_all(User user,long employeeId)
        {

                var result = DBManager.Instance.selectExtras(employeeId);
                return Responses.SimpleWithData(result.Status,result.Detail);

      
        }
        /// <summary>
        /// Accesa a un pago por su identificador
        /// </summary>
        /// <param name="extraId">Identificador del pago extra</param>
        /// <returns>
        /// Pago extra asociado al identificador
        /// Estado de la transaccion en formato JSON
        /// </returns>
        public static Response get(User user,long extraId)
        {

                var result = DBManager.Instance.selectExtra(extraId);
                return Responses.SimpleWithData(result.Status, result.Detail);

        }
    }
}