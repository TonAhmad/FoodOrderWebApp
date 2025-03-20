using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using Project2.Models;

namespace Project2
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginAdmin userLogin = new LoginAdmin
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text
            };

            if (userLogin.Authenticate())
            {
                Session["Username"] = userLogin.Username;
                Session["Fullname"] = userLogin.Fullname;
                Session["Role"] = userLogin.Role;

                if (userLogin.Role == "admin")
                {
                    Response.Redirect("_Admin/Dashboard.aspx");
                }
                else if (userLogin.Role == "cashier")
                {
                    Response.Redirect("_Cashier/ProcessPayment.aspx");
                }
            }
            else
            {
                lblMessage.Text = "Invalid username or password!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}