using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace Project2
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Jika sudah login, arahkan ke halaman dashboard yang sesuai
                if (Session["Username"] != null)
                {
                    Response.Redirect(Session["Role"].ToString() == "admin" ? "_Admin/Dashboard.aspx" : "_Cashier/DashboardC.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Ambil connection string dari Web.config
            string connString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT role FROM adm.Admin WHERE username = @username AND password_hash = @password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string role = result.ToString().ToLower(); // Pastikan role dalam lowercase

                        if (role == "admin" || role == "cashier")
                        {
                            // Simpan session login
                            Session["Username"] = username;
                            Session["Role"] = role;

                            // Redirect sesuai role
                            Response.Redirect(role == "admin" ? "_Admin/Dashboard.aspx" : "_Cashier/DashboardC.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Access denied! Only Admin and Cashier can log in.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Invalid username or password!";
                    }
                }
            }
        }
    }
}