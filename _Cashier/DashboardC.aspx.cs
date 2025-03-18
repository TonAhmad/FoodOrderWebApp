using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Project2._Cashier
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            string connString = ConfigurationManager.ConnectionStrings["Set"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT orderID, productID, quantity, subtotal, (quantity * subtotal) AS TotalAmount 
                                 FROM orders.OrderDetail"; // Ambil data pesanan customer

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                da.Fill(dt);
                conn.Close();

                gvOrders.DataSource = dt;
                gvOrders.DataBind();

                // Hitung total belanja
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["Subtotal"]);
                }
                lblTotal.Text = $"Rp {total:N0}";
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Session["TotalAmount"] = lblTotal.Text.Replace("Rp ", "").Replace(",", "").Trim();
            Response.Redirect("ProcessPayment.aspx");
        }
    }
}
