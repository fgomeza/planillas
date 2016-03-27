using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Models
{
    public class DataBase<K> : IDisposable where K : DbConnection,new()
    {
        private K conn;

        public DataBase (String connectionString)
        {
            conn = new K();
            conn.ConnectionString = connectionString;
        }

        public K Open()
        {
            if(conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public void Dispose()
        {
            if(conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}