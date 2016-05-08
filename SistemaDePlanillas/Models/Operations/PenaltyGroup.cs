using SistemaDePlanillas.Models.Manager;
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
        
        public static Penalty add(User user, long employee, string detail, long amount, long months, long penalty_type, DateTime date)
        {
            return DBManager.Instance.penalties.addPenalty(employee, detail, amount, months, penalty_type, date);
        }

        
        /// <summary>
        /// Actualiza la informacion de una penalizacion
        /// </summary>
        /// <param name="penaltyId">Identificador de la penalizacion</param>
        /// <param name="detail">Motivo de la penalizacion</param>
        /// <param name="amount">Monto a descontar</param>
        /// <returns>Estado de la transaccion en formato JSON</returns>
        
        public static void modify(User user, long id_recess,long payroll ,long penalty_type, string detail, long amount, DateTime date)
        {
            DBManager.Instance.penalties.updatePenalty(id_recess,payroll, penalty_type, detail, amount, date);
        }
                
        
        /// <summary>
        /// Elimina una penalizacion asociada a un empleado
        /// </summary>
        /// <param name="recessId">Identificador de la penalizacion</param>
        /// <returns>>Estado de la transaccion en formato JSON</returns>
        
        public static void remove(User user, long id_recess)
        {
            DBManager.Instance.penalties.deletePenalty(id_recess);
        }
        
        /// <summary>
        /// Accesa a una penalizacion por su identificador
        /// </summary>
        /// <param name="extraId">Identificador de la penalizacion</param>
        /// <returns>
        /// Penalizacion asociada al identificador
        /// Estado de la transaccion en formato JSON
        /// </returns>
        
        public static object get(User user, long recces_id)
        {
            return DBManager.Instance.penalties.selectAllPenalty(recces_id,DateTime.Now);
        }

        
        /// <summary>
        /// Accesa a las penalizaciones vinculadas a un empleado
        /// </summary>
        /// <param name="employeeId">Identificador del empleado</param>
        /// <returns>
        /// Lista con las penalizaciones asociadas al usuario
        /// Estado de la transaccion en formato JSON
        /// </returns>
        
        public static object get_all(User user, long employee)
        {
            return DBManager.Instance.penalties.selectAllPenalty(employee, DateTime.Now);
        }

        public static void pay(User user, long payroll_id, long employee_id ,DateTime endDate)
        {
            DBManager.Instance.penalties.payPenalty(payroll_id, employee_id, endDate);
        }
    }
}