using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("../Login.aspx"); // Redirect ke login jika belum login
            }
            else
            {
                lblUsername.Text = "Welcome, " + Session["Fullname"].ToString();
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();  // Hapus semua session
            Session.Abandon(); // Hapus session dari server
            Response.Redirect("Login.aspx"); // Redirect ke halaman login
        }
    }
}