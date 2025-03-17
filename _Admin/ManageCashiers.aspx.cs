using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2.Models;

namespace Project2._Admin
{
    public partial class ManageCashiers : System.Web.UI.Page
    {

        AddAdmin admin = new AddAdmin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            gvAdmins.DataSource = admin.Read();
            gvAdmins.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            admin.username = txtUsername.Text;
            admin.fullname = txtFullname.Text;
            admin.email = txtEmail.Text;
            admin.password_hash = txtPassword.Text;
            admin.phone_number = txtPhone.Text;
            admin.address = txtAddress.Text;
            admin.role = ddlRole.SelectedValue;

            string result = admin.Create();
            Response.Write("<script>alert('" + result + "');</script>");
            LoadData();
            ClearData();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            admin.username = txtUsername.Text;
            admin.fullname = txtFullname.Text;
            admin.email = txtEmail.Text;
            admin.phone_number = txtPhone.Text;
            admin.address = txtAddress.Text;

            string result = admin.Update();
            Response.Write("<script>alert('" + result + "');</script>");
            LoadData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            admin.username = txtUsername.Text;
            string result = admin.Delete();
            Response.Write("<script>alert('" + result + "');</script>");
            LoadData();
        }

        protected void gvAdmins_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvAdmins_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        void ClearData()
        {
            txtFullname.Text = "";
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }

        protected void gvAdmins_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ambil data dari baris yang dipilih
            GridViewRow row = gvAdmins.SelectedRow;

            // Masukkan nilai ke dalam TextBox
            txtUsername.Text = row.Cells[1].Text;  // Sesuaikan index dengan kolom Username
            txtFullname.Text = row.Cells[2].Text;
            txtEmail.Text = row.Cells[3].Text;
            txtPhone.Text = row.Cells[4].Text;
            txtAddress.Text = row.Cells[5].Text;
        }
    }
}