using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();  // Hapus semua session
            Session.Abandon(); // Hapus session dari server
            Response.Redirect("~/Login.aspx"); // Redirect ke halaman login
        }
    }
}