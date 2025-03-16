using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2
{
    public partial class TesKoneksi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    lblMessage.Text = "✅ Koneksi ke database berhasil!";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "❌ Koneksi gagal: " + ex.Message;
                }

            }
        }
    }
}