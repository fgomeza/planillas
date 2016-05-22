using SistemaDePlanillas.Models.Manager;
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
        public static Extra add(User user,long employeeId, string detail,long hours)
        {
               return DBManager.Instance.extras.addExtra(employeeId, detail, hours);
        }
        /// <summary>
        /// Elimina un pago extra
        /// </summary>
        /// <param name="extraId">identificador del pago extra</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
        public static void remove(User user,long extraId)
        {
                DBManager.Instance.extras.deleteExtra(extraId);
        }
        /// <summary> 
        /// Actualiza la informacio de un pago extra
        /// </summary>
        /// <param name="extraId">Identificador del pago extra</param>
        /// <param name="detail">Motivo del pago</param>
        /// <param name="amount">Cantidad a pagar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
        public static void modify(User user,long extraId, string detail, long hours)
        {
                DBManager.Instance.extras.updateExtra(extraId, detail, hours);
        }
        /// <summary>
        /// Accesa los pagos extra asociados a un empleado
        /// </summary>
        /// <param name="employeeId">Identificador del empleado</param>
        /// <returns>
        /// Lista con los cagos extra asociados al empleado
        /// Estado de la transaccion en formato JSON
        /// </returns>
        public static object get_all(User user,long employeeId)
        {
                return DBManager.Instance.extras.selectExtras(employeeId, DateTime.Now);  
        }
        /// <summary>
        /// Accesa a un pago por su identificador
        /// </summary>
        /// <param name="extraId">Identificador del pago extra</param>
        /// <returns>
        /// Pago extra asociado al identificador
        /// Estado de la transaccion en formato JSON
        /// </returns>
        public static object get(User user,long extraId)
        {
            return DBManager.Instance.extras.selectExtra(extraId);
        }
    }
}