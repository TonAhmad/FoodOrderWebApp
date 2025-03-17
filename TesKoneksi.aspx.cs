using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2.Models;
using System.Data;

namespace Project2
{
    public partial class TesKoneksi : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = Koneksi.connString;
                con.Open();
                Response.Write("Koneksi Berhasil<br/>");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    Response.Write("Koneksi Ditutup");
                    con = null;
                }
            }
        }
    }
}