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
    public class ExtraGroup
    {
        /// <summary>
        /// Genera un pago extra y lo asocia a un empleado
        /// </summary>
        /// <param name="employeeId">identificador del empleado</param>
        /// <param name="detail">Motivo del pago</param>
        /// <param name="amount">Cantidad a pagar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
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
        /// <summary>
        /// Elimina un pago extra
        /// </summary>
        /// <param name="extraId">identificador del pago extra</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
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
        /// <summary>
        /// Actualiza la informacio de un pago extra
        /// </summary>
        /// <param name="extraId">Identificador del pago extra</param>
        /// <param name="detail">Motivo del pago</param>
        /// <param name="amount">Cantidad a pagar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
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
        /// <summary>
        /// Accesa los pagos extra asociados a un empleado
        /// </summary>
        /// <param name="employeeId">Identificador del empleado</param>
        /// <returns>
        /// Lista con los cagos extra asociados al empleado
        /// Estado de la transaccion en formato JSON
        /// </returns>
        public string ListAllExtras(long employeeId)
        {
            try
            {
                List<Extra> allExtras = DBManager.getInstance().selectExtras((int)employeeId).detail;
                return "";
            }
            catch (Exception)
            {

                return "";
            }
      
        }
        /// <summary>
        /// Accesa a un pago por su identificador
        /// </summary>
        /// <param name="extraId">Identificador del pago extra</param>
        /// <returns>
        /// Pago extra asociado al identificador
        /// Estado de la transaccion en formato JSON
        /// </returns>
        public string FindExtraById(long extraId)
        {
            try
            {
                Extra extra = DBManager.getInstance().selectExtra((int)extraId).detail;
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}