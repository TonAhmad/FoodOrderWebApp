using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    internal class Koneksi
    {
        SqlConnection con;

        public Koneksi()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            con = new SqlConnection(connectionString);
        }

        public SqlConnection bukaKoneksi()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public void tutupKoneksi()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}