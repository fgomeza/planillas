﻿using SistemaDePlanillas.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models.Operations
{
    public class DebitsGroup
    {
        public static Debit add_Fixed(User user, long employee, string detail, double amount, long type, long period)
        {
            return DBManager.Instance.debits.addFixedDebit(employee, detail, amount, type, period);
        }

        public static PaymentDebit add_Payment(User user, long employee, DateTime initialDate, string detail, double total, double interestRate, long pays, long type,long period)
        {
            return DBManager.Instance.debits.addPaymentDebit(employee, initialDate, detail, total, pays, type,period);
        }
        public static AmortizationDebit add_Amortization(User user, long employee, DateTime initialDate, string detail, double total, double interestRate, long pays, long type, long period)
        {
            return DBManager.Instance.debits.addAmortizationDebit(employee, detail, total, pays, type,period);
        }

        public static List<Debit> get_AllFixed(User user, long employee)
        {
            return DBManager.Instance.debits.selectActiveFixedDebits(employee);
        }

        public static List<PaymentDebit> get_AllPayment(User user, long employee)
        {
            return DBManager.Instance.debits.selectPaymentDebits(employee);
        }

        public static List<AmortizationDebit> get_AllAmortization(User user, long employee)
        {
            return DBManager.Instance.debits.selectAmortizationDebits(employee);
        }

        public static Debit get_Fixed(User user, long idDebit)
        {
            return DBManager.Instance.debits.selectFixedDebit(idDebit);
        }

        public static PaymentDebit get_Payment(User user, long idDebit)
        {
            return DBManager.Instance.debits.selectPaymentDebit(idDebit);  
        }

        public static AmortizationDebit get_Amortization(User user, long idDebit)
        {
            return DBManager.Instance.debits.selectAmortizationDebit(idDebit);
        }

        public static void modify_Fixed(User user, long idDebit, string detail, double amount, long period)
        {
            DBManager.Instance.debits.updateFixedDebit(idDebit, detail, amount, period);
        }

        public static void modify_Payment(User user, long idDebit, double total, long remainingMonths, long period)
        {
            DBManager.Instance.debits.updatePaymentDebit(idDebit, total, remainingMonths,period);
        }

        public static void remove_Fixed(User user, long idDebit)
        {
            DBManager.Instance.debits.deleteDebit(idDebit);
        }

        public static void remove_Payment(User user, long idDebit)
        {
            DBManager.Instance.debits.deleteDebit(idDebit);
        }

        public static void remove_Amortization(User user, long idDebit)
        {
            DBManager.Instance.debits.deleteDebit(idDebit);
        }

    }
}