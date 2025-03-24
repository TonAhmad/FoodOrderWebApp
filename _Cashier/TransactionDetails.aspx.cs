using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Project2._Cashier
{
    public partial class TransactionDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Pastikan parameter orderID ada di URL
                if (Request.QueryString["orderID"] != null)
                {
                    string orderID = Request.QueryString["orderID"];
                    lblTransactionID.Text = orderID; // Tampilkan ID transaksi di halaman
                    LoadTransactionDetails(orderID);
                }
                else
                {
                    Response.Redirect("TransactionHistory.aspx"); // Jika tidak ada orderID, kembali ke history
                }
            }
        }

        private void LoadTransactionDetails(string orderID)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Ambil detail transaksi utama dari OrderHeader
                string headerQuery = "SELECT orderDate, (SELECT SUM(subtotal) FROM orders.OrderDetail WHERE orderID = @orderID) AS totalAmount FROM orders.OrderHeader WHERE orderID = @orderID";
                using (SqlCommand cmd = new SqlCommand(headerQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@orderID", orderID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblTransactionDate.Text = Convert.ToDateTime(reader["orderDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        lblTotalAmount.Text = "Rp " + Convert.ToDecimal(reader["totalAmount"]).ToString("N0");
                    }
                    reader.Close();
                }

                // Ambil detail item transaksi dari OrderDetail dengan JOIN ke Product
                string detailQuery = @"SELECT p.productName, d.quantity, (d.subtotal / d.quantity) AS unitPrice, d.subtotal FROM orders.OrderDetail dJOIN item.Product p ON d.productID = p.productID WHERE d.orderID = @orderID";

                using (SqlDataAdapter da = new SqlDataAdapter(detailQuery, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@orderID", orderID);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvTransactionDetails.DataSource = dt;
                    gvTransactionDetails.DataBind();
                }
            }
        }
    }
}
