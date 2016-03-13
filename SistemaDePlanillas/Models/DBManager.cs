using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace SistemaDePlanillas.Models

{
    public class DBManager
    {
        private static long OK = 0;
        private static long DBERR = 18;
        private static DBManager instance;
        private NpgsqlConnection cnx;
        private string stringConnect = "Server=localhost;Port=5432;Database=COOPESUPERACION;User Id=postgres;Password=root;";
        //private string stringConnect = "Server=localhost;Port=5432;Database=planillas;User Id=postgres;Password=postgres;";

        private DBManager() { }

        public static DBManager Instance
        {
            get
            {
                return instance == null ? (instance = new DBManager()) : instance;
            }
        }

        private bool connect()
        {
            try
            {
                cnx = new NpgsqlConnection(stringConnect);
                cnx.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Result<string> addCmsEmployee(string idCard, string CMS, string name, long location, string account)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[0].Value = idCard;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = CMS;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[2].Value = name;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[3].Value = location;
                    command.Parameters[4].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[4].Value = account;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);

                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try {res.status = long.Parse(e.MessageText);}
                    catch (Exception){throw e;}
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updateCmsEmployeee(long id, string idCard, string CMS, string name, long location, string account)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = idCard;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[2].Value = CMS;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[3].Value = name;
                    command.Parameters[4].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[4].Value = location;
                    command.Parameters[5].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[5].Value = account;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);

                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Employee>> selectAllCmsEmployees(long location)
        {
            Result<List<Employee>> res = new Result<List<Employee>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_08", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = location;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    res.detail = new List<Employee>();
                    while (dr.Read())
                    {
                        Employee emp = new Employee();
                        emp.id = dr.GetInt64(0);
                        emp.idCard = dr.GetString(1);
                        emp.name = dr.GetString(2);
                        emp.location = dr.GetString(3);
                        emp.account = dr.GetString(4);
                        emp.cmsText = dr.GetString(5);
                        emp.calls = dr.GetInt64(6);
                        res.detail.Add(emp);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updateCalls(long id, long calls)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_10", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[1].Value = calls;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = 18;
            }
            return res;
        }

        public Result<string> addNonCmsEmployee(string idCard, string name, long location, string account, float salary)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[0].Value = idCard;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = name;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[2].Value = location;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[3].Value = account;
                    command.Parameters[4].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[4].Value = salary;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = 18;
            }
            return res;
        }

        public Result<string> updateNonCmsEmployeee(long id, string idCard, string name, long location, string account, float salary)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_04", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = idCard;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[2].Value = name;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[3].Value = location;
                    command.Parameters[4].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[4].Value = account;
                    command.Parameters[5].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[5].Value = salary;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = 18;
            }
            return res;
        }

        public Result<List<Employee>> selectAllNonCmsEmployees(long location)
        {
            Result<List<Employee>> res = new Result<List<Employee>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_09", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = location;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    res.detail = new List<Employee>();
                    while (dr.Read())
                    {
                        Employee emp = new Employee();
                        emp.id = dr.GetInt64(0);
                        emp.idCard = dr.GetString(1);
                        emp.name = dr.GetString(2);
                        emp.location = dr.GetString(3);
                        emp.account = dr.GetString(4);
                        emp.salary = dr.GetDouble(5);
                        res.detail.Add(emp);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deleteEmployee(long id)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_05", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<Employee> selectEmployee(long id)
        {
            Result<Employee> res = new Result<Employee>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_06", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        Employee emp = new Employee();
                        emp.id = id;
                        emp.idCard = dr.GetString(0);
                        emp.name = dr.GetString(1);
                        emp.location = dr.GetString(2);
                        emp.account = dr.GetString(3);
                        emp.cms = dr.GetBoolean(4);
                        res.detail = emp;
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Employee>> selectAllEmployees(long location)
        {
            Result<List<Employee>> res = new Result<List<Employee>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTR_07", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = location;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    res.detail = new List<Employee>();
                    while (dr.Read())
                    {
                        Employee emp = new Employee();
                        emp.id = dr.GetInt64(0);
                        emp.idCard = dr.GetString(1);
                        emp.name = dr.GetString(2);
                        emp.location = dr.GetString(3);
                        emp.account = dr.GetString(4);
                        emp.cms = dr.GetBoolean(5);
                        res.detail.Add(emp);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> addDebit(long employee, string detail, double amount, long type)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDF_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = employee;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = detail;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[2].Value = amount;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[3].Value = type;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updateDebit(long idDebit, string detail, double amount)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDF_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idDebit;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = detail;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[2].Value = amount;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deleteDebit(long idDebit)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDF_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idDebit;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<Debit> selectDebit(long idDebit)
        {
            Result<Debit> res = new Result<Debit>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDF_04", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idDebit;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Debit deb = new Debit();
                        deb.id = dr.GetInt64(0);
                        deb.employee = dr.GetInt64(1);
                        deb.detail = dr.GetString(2);
                        deb.amount = dr.GetDouble(3);
                        deb.type = dr.GetInt64(4);
                        res.detail = deb;
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Debit>> selectDebits(long employee)
        {
            Result<List<Debit>> res = new Result<List<Debit>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDF_05", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = employee;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    res.detail = new List<Debit>();
                    while (dr.Read())
                    {
                        Debit deb = new Debit();
                        deb.id = dr.GetInt64(0);
                        deb.employee = dr.GetInt64(1);
                        deb.detail = dr.GetString(2);
                        deb.amount = dr.GetDouble(3);
                        deb.type = dr.GetInt64(4);
                        res.detail.Add(deb);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> addPaymentDebit(long employee, string detail, double total, double interestRate, long months, long type)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDC_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = employee;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = detail;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[2].Value = total;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[3].Value = interestRate;
                    command.Parameters[4].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[4].Value = months;
                    command.Parameters[5].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[5].Value = type;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updatePaymentDebit(long idDebit, string detail, float total, double interestRate, long months, double remainingDebt)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDC_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idDebit;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = detail;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[2].Value = total;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[3].Value = interestRate;
                    command.Parameters[4].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[4].Value = months;
                    command.Parameters[5].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[5].Value = remainingDebt;


                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deletePaymentDebit(long idDebit)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDC_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idDebit;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<PaymentDebit> selectPaymentDebit(long idDebit)
        {
            Result<PaymentDebit> res = new Result<PaymentDebit>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDC_04", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idDebit;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        PaymentDebit Pdeb = new PaymentDebit();
                        Pdeb.id = dr.GetInt64(0);
                        Pdeb.employee = dr.GetInt64(1);
                        Pdeb.detail = dr.GetString(2);
                        Pdeb.total = dr.GetDouble(3);
                        Pdeb.interestRate = dr.GetDouble(4);
                        Pdeb.paymentsMade = dr.GetInt64(5);
                        Pdeb.missingPayments = dr.GetInt64(6);
                        Pdeb.remainingDebt = dr.GetDouble(7);
                        Pdeb.type = dr.GetInt64(8);
                        res.detail = Pdeb;
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = 0;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<PaymentDebit>> selectPaymentDebits(long employee)
        {
            Result<List<PaymentDebit>> res = new Result<List<PaymentDebit>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDC_05", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = employee;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<PaymentDebit>();
                    while (dr.Read())
                    {
                        PaymentDebit Pdeb = new PaymentDebit();
                        Pdeb.id = dr.GetInt64(0);
                        Pdeb.employee = dr.GetInt64(1);
                        Pdeb.detail = dr.GetString(2);
                        Pdeb.total = dr.GetDouble(3);
                        Pdeb.interestRate = dr.GetDouble(4);
                        Pdeb.paymentsMade = dr.GetInt64(5);
                        Pdeb.missingPayments = dr.GetInt64(6);
                        Pdeb.remainingDebt = dr.GetDouble(7);
                        Pdeb.type = dr.GetInt64(8);
                        res.detail.Add(Pdeb);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = 18;
            }
            return res;
        }

        public Result<String> payDebit(long idDebit, float amount)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FDC_07", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idDebit;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[1].Value = amount;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> addExtra(long employee, string detail, float amount)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FEX_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = employee;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = detail;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[2].Value = amount;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updateExtra(long idExtra, string detail, float amount)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FEX_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idExtra;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = detail;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[2].Value = amount;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deleteExtra(long idExtra)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FEX_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idExtra;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<Extra> selectExtra(long idExtra)
        {
            Result<Extra> res = new Result<Extra>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FEX_04", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idExtra;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Extra ext = new Extra();
                        ext.id = dr.GetInt64(0);
                        ext.employee = dr.GetInt64(1);
                        ext.detail = dr.GetString(2);
                        ext.amount = dr.GetDouble(3);
                        res.detail = ext;
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Extra>> selectExtras(long employee)
        {
            Result<List<Extra>> res = new Result<List<Extra>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FEX_05", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = employee;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<Extra>();
                    while (dr.Read())
                    {
                        Extra ext = new Extra();
                        ext.id = dr.GetInt64(0);
                        ext.employee = dr.GetInt64(1);
                        ext.detail = dr.GetString(2);
                        ext.amount = dr.GetDouble(3);
                        res.detail.Add(ext);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> addRecess(long employee, string detail, float amount, long months)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FMU_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = employee;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = detail;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[2].Value = amount;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[3].Value = months;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);

                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updateRecess(long idRecess, string detail, double amount, long months, double remainingDebt)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FMU_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idRecess;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = detail;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[2].Value = amount;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[3].Value = months;
                    command.Parameters[4].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[4].Value = remainingDebt;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deleteRecess(long idRecess)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FMU_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idRecess;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    return res;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<Recess> selectRecess(long idRecess)
        {
            Result<Recess> res = new Result<Recess>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FMU_04", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idRecess;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Recess recess = new Recess();
                        recess.id = dr.GetInt64(0);
                        recess.employee = dr.GetInt64(1);
                        recess.detail = dr.GetString(2);
                        recess.amount = dr.GetDouble(3);
                        recess.paymentsMade = dr.GetInt64(4);
                        recess.missingPayments = dr.GetInt64(5);
                        recess.remainingRecess = dr.GetDouble(6);
                        res.detail = recess;
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Recess>> selectAllRecess(long employee)
        {
            Result<List<Recess>> res = new Result<List<Recess>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FMU_05", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = employee;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<Recess>();
                    while (dr.Read())
                    {
                        Recess recess = new Recess();
                        recess.id = dr.GetInt64(0);
                        recess.employee = dr.GetInt64(1);
                        recess.detail = dr.GetString(2);
                        recess.amount = dr.GetDouble(3);
                        recess.paymentsMade = dr.GetInt64(4);
                        recess.missingPayments = dr.GetInt64(5);
                        recess.remainingRecess = dr.GetDouble(6);
                        res.detail.Add(recess);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<String> payRecess(long idRecess, float amount)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FMU_07", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idRecess;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[1].Value = amount;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;

        }

        public Result<string> addPayroll(DateTime date, long user, string file, long location)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FPL_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Date;
                    command.Parameters[0].Value = date;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[1].Value = user;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[2].Value = file;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[3].Value = location;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updatePayroll(long idPay, DateTime date, long user, string file)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FPL_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idPay;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Date;
                    command.Parameters[1].Value = date;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[2].Value = user;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[3].Value = file;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deletePayroll(long idPay)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FPL_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idPay;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<Payroll> selectPayroll(long idPay)
        {
            Result<Payroll> res = new Result<Payroll>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FPL_04", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idPay;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Payroll pay = new Payroll();
                        pay.id = dr.GetInt64(0);
                        pay.date = dr.GetDate(1);
                        pay.user = dr.GetInt64(2);
                        pay.file = dr.GetString(3);
                        res.detail = pay;
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Payroll>> selectAllPayroll(long idPay)
        {
            Result<List<Payroll>> res = new Result<List<Payroll>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FPL_05", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = idPay;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<Payroll>();
                    while (dr.Read())
                    {
                        Payroll pay = new Payroll();
                        pay.id = dr.GetInt64(0);
                        pay.date = dr.GetDate(1);
                        pay.user = dr.GetInt64(2);
                        res.detail.Add(pay);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Payroll>> selectPayroll(DateTime ini, DateTime end)
        {
            Result<List<Payroll>> res = new Result<List<Payroll>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FPL_06", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Date;
                    command.Parameters[0].Value = ini;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Date;
                    command.Parameters[1].Value = end;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<Payroll>();
                    while (dr.Read())
                    {
                        Payroll pay = new Payroll();
                        pay.id = dr.GetInt64(0);
                        pay.date = dr.GetDate(1);
                        pay.user = dr.GetInt64(2);
                        pay.file = dr.GetString(3);
                        res.detail.Add(pay);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> addLocation(string name)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FSE_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[0].Value = name;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updateLocation(long id, string name)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FSE_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = name;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.detail = dr.GetString(0);

                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deleteLocation(long id)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FSE_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);

                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Location>> selectLocations()
        {
            Result<List<Location>> res = new Result<List<Location>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FSE_04", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<Location>();
                    while (dr.Read())
                    {
                        Location loc = new Location();
                        loc.id = dr.GetInt64(0);
                        loc.name = dr.GetString(1);
                        res.detail.Add(loc);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> addRole(string name, long location, List<Tuple<string, string>> privileges) // group-operation
        {
            Result<string> res = new Result<string>();
            var result = addRole(name, location);
            if (result.status != OK)
            {
                res.status = result.status;
                return res;
            }
            foreach (var x in privileges)
            {
                var res2 = Privilege(result.detail, x.Item1, x.Item2);
                if (res2.status != OK)
                {
                    deleteRole(result.detail);
                    res.status = res2.status;
                    return res;
                }
            }
            res.status = OK;
            return res;
        }

        private Result<long> addRole(string name, long location)
        {
            Result<long> res = new Result<long>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FRO_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[0].Value = name;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[1].Value = location;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.detail = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updateRole(long id, string name, List<Tuple<string, string>> privileges)
        {
            Result<string> res = updateRole(id,name);
            if (res.status == OK)
            {
                foreach (var x in privileges)
                {
                   Privilege(id, x.Item1, x.Item2);
                }
            }
            return res;
        }

        private Result<string> updateRole(long id, string name)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FRO_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = name;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deleteRole(long id)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FRO_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Role>> selectRoles()
        {
            Result<List<Role>> res = new Result<List<Role>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FRO_04", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<Role>();
                    while (dr.Read())
                    {
                        long id = dr.GetInt64(0);
                        string name = dr.GetString(1);
                        long location = dr.GetInt64(2);
                        res.detail.Add(new Role(id, name, location, selectRolePrivileges(id).detail));
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        private Result<string> Privilege(long role, string group, string privilege)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FOP_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = role;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = privilege;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[2].Value = group;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Tuple<string, string>>> selectRolePrivileges(long role) //group-operation
        {
            Result<List<Tuple<string, string>>> res = new Result<List<Tuple<string, string>>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FOP_03", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = role;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<Tuple<string, string>>();
                    while (dr.Read())
                    {
                        string group = dr.GetString(0);
                        string op = dr.GetString(1);
                        res.detail.Add(new Tuple<string, string>(group, op));
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<OperationsGroup>> selectOperationsGroups()
        {
            Result<List<OperationsGroup>> res = new Result<List<OperationsGroup>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FGO_01", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<OperationsGroup>();
                    while (dr.Read())
                    {
                        string name = dr.GetString(0);
                        string desc = dr.GetString(1);
                        string icon = dr.GetString(2);
                        bool align = dr.GetBoolean(3);
                        res.detail.Add(new OperationsGroup(desc, name, icon, align, selectAllOperations(name).detail));
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Operation>> selectAllOperations(string groupName)
        {
            Result<List<Operation>> res = new Result<List<Operation>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FOP_05", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[0].Value = groupName;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<Operation>();
                    while (dr.Read())
                    {
                        string name = dr.GetString(0);
                        string desc = dr.GetString(1);
                        res.detail.Add(new Operation(desc, name, groupName));
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                    return res;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> addUser(string name, string username, string password, long role, long location, string email)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FUS_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[0].Value = name;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = username;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[2].Value = password;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[3].Value = role;
                    command.Parameters[4].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[4].Value = location;
                    command.Parameters[5].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[5].Value = email;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updateUser(long id, string name, string username, string password, long role, long location, string email)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FUS_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = name;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[2].Value = username;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[3].Value = password;
                    command.Parameters[4].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[4].Value = role;
                    command.Parameters[5].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[5].Value = location;
                    command.Parameters[6].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[6].Value = email;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deleteUser(long id)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FUS_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<User> selectUser(long id)
        {
            Result<User> res = new Result<User>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FUS_04", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        User us = new User();
                        us.id = dr.GetInt64(0);
                        us.name = dr.GetString(1);
                        us.username = dr.GetString(2);
                        us.password = dr.GetString(3);
                        us.role = dr.GetInt64(4);
                        us.location = dr.GetInt64(5);
                        us.email = dr.GetString(6);
                        res.detail = us;
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<User>> selectAllUsers(long location)
        {
            Result<List<User>> res = new Result<List<User>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FUS_05", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = location;
                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<User>();
                    while (dr.Read())
                    {
                        User us = new User();
                        us.id = dr.GetInt64(0);
                        us.name = dr.GetString(1);
                        us.username = dr.GetString(2);
                        us.password = dr.GetString(3);
                        us.role = dr.GetInt64(4);
                        us.location = dr.GetInt64(5);
                        us.email = dr.GetString(6);
                        res.detail.Add(us);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<User> login(string username, string password)
        {
            Result<User> res = new Result<User>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FUS_06", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[0].Value = username;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = password;

                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        res.detail = selectUser(dr.GetInt64(0)).detail;
                    }

                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<Tuple<long, string>>> selectAllErrors()
        {
            Result<List<Tuple<long, string>>> res = new Result<List<Tuple<long, string>>>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FERR_04", cnx);
                    command.Transaction = tran;
                    command.CommandType = CommandType.StoredProcedure;
                    NpgsqlDataReader dr = command.ExecuteReader();
                    res.detail = new List<Tuple<long, string>>();
                    while (dr.Read())
                    {
                        Tuple<long, string> t = new Tuple<long, string>(dr.GetInt64(0), dr.GetString(1));
                        res.detail.Add(t);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                    res.status = OK;
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> addFixedDebitType(string name, long location)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTDF_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[0].Value = name;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[1].Value = location;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deleteFixedDebitType(long id)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTDF_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<DebitType>> selectFixedDebitTypes(long location)
        {
            Result<List<DebitType>> res = new Result<List<DebitType>>();
            res.detail = new List<DebitType>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTDF_04", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = location;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        DebitType dt = new DebitType();
                        dt.id = dr.GetInt64(0);
                        dt.name = dr.GetString(1);
                        res.detail.Add(dt);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> addPaymentDebitType(string name, float interestRate, long months, long location)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTDF_01", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[0].Value = name;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[1].Value = interestRate;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[2].Value = months;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[3].Value = location;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> updatePaymentDebitType(long id, string name, float interestRate, long months)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTDC_02", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;
                    command.Parameters[1].NpgsqlDbType = NpgsqlDbType.Text;
                    command.Parameters[1].Value = name;
                    command.Parameters[2].NpgsqlDbType = NpgsqlDbType.Numeric;
                    command.Parameters[2].Value = interestRate;
                    command.Parameters[3].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[3].Value = months;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<string> deletePaymentDebitType(long id)
        {
            Result<string> res = new Result<string>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTDC_03", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = id;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                        res.status = dr.GetInt64(0);
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }

        public Result<List<DebitType>> selectPaymentDebitTypes(long location)
        {
            Result<List<DebitType>> res = new Result<List<DebitType>>();
            res.detail = new List<DebitType>();
            if (connect())
            {
                try
                {
                    NpgsqlTransaction tran = cnx.BeginTransaction();
                    NpgsqlCommand command = new NpgsqlCommand("FTDC_04", cnx);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter());

                    command.Parameters[0].NpgsqlDbType = NpgsqlDbType.Bigint;
                    command.Parameters[0].Value = location;

                    NpgsqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        DebitType dt = new DebitType();
                        dt.id = dr.GetInt64(0);
                        dt.name = dr.GetString(1);
                        dt.interestRate = dr.GetFloat(2);
                        dt.months = dr.GetInt64(3);
                        res.detail.Add(dt);
                    }
                    dr.Close();
                    tran.Commit();
                    cnx.Close();
                }
                catch (NpgsqlException e)
                {
                    cnx.Close();
                    try { res.status = long.Parse(e.MessageText); }
                    catch (Exception) { throw e; }
                }
            }
            else
            {
                res.status = DBERR;
            }
            return res;
        }
    }

}