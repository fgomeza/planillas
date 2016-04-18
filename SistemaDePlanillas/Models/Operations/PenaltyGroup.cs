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
        
        public static Response add(User user, long employee, string detail, long amount, long months, long penalty_type, DateTime date)
        {
            var result = DBManager.Instance.addPenalty(employee, detail, amount, months, penalty_type, date);
            return Responses.Simple(result.Status);
        }

        
        /// <summary>
        /// Actualiza la informacion de una penalizacion
        /// </summary>
        /// <param name="penaltyId">Identificador de la penalizacion</param>
        /// <param name="detail">Motivo de la penalizacion</param>
        /// <param name="amount">Monto a descontar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
        
        public static Response modify(User user, long id_recess,long payroll ,long penalty_type, string detail, long amount, DateTime date)
        {
            var result = DBManager.Instance.updatePenalty(id_recess,payroll, penalty_type, detail, amount, date);
            return Responses.Simple(result.Status);
        }
                
        
        /// <summary>
        /// Elimina una penalizacion asociada a un empleado
        /// </summary>
        /// <param name="recessId">Identificador de la penalizacion</param>
        /// <returns>>Estado de la transaccion en formato JSON</returns>
        
        public static Response remove(User user, long id_recess)
        {
            var result = DBManager.Instance.deletePenalty(id_recess);
            return Responses.Simple(result.Status);
        }
        
        /// <summary>
        /// Accesa a una penalizacion por su identificador
        /// </summary>
        /// <param name="extraId">Identificador de la penalizacion</param>
        /// <returns>
        /// Penalizacion asociada al identificador
        /// Estado de la transaccion en formato JSON
        /// </returns>
        
        public static Response get(User user, long recces_id)
        {
            var result = DBManager.Instance.selectAllPenalty(recces_id,DateTime.Now);
            return Responses.WithData(result.Detail);
        }

        
        /// <summary>
        /// Accesa a las penalizaciones vinculadas a un empleado
        /// </summary>
        /// <param name="employeeId">Identificador del empleado</param>
        /// <returns>
        /// Lista con las penalizaciones asociadas al usuario
        /// Estado de la transaccion en formato JSON
        /// </returns>
        
        public static Response get_all(User user, long employee)
        {
            var result = DBManager.Instance.selectAllPenalty(employee, DateTime.Now);
            return Responses.WithData(result.Detail);
        }

        public static Response pay(User user, long payroll_id, long employee_id ,DateTime endDate)
        {
            var result = DBManager.Instance.payPenalty(payroll_id, employee_id, endDate);
            return Responses.Simple(result.Status);
        }
    }
}