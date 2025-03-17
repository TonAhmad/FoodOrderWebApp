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
                if (Request.QueryString["TransactionID"] != null)
                {
                    string transactionID = Request.QueryString["TransactionID"];
                    LoadTransactionDetails(transactionID);
                }
                else
                {
                    Response.Redirect("TransactionHistory.aspx");
                }
            }
        }

        private void LoadTransactionDetails(string transactionID)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                // Ambil data transaksi utama
                string queryTransaction = "SELECT TransactionID, TransactionDate, TotalAmount FROM Transactions WHERE TransactionID = @TransactionID";
                SqlCommand cmdTransaction = new SqlCommand(queryTransaction, conn);
                cmdTransaction.Parameters.AddWithValue("@TransactionID", transactionID);

                conn.Open();
                SqlDataReader reader = cmdTransaction.ExecuteReader();
                if (reader.Read())
                {
                    lblTransactionID.Text = reader["TransactionID"].ToString();
                    lblTransactionDate.Text = Convert.ToDateTime(reader["TransactionDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    lblTotalAmount.Text = $"Rp {Convert.ToDecimal(reader["TotalAmount"]):N0}";
                }
                reader.Close();

                // Ambil detail produk dalam transaksi
                string queryDetails = "SELECT ProductName, Quantity, Price, (Quantity * Price) AS Subtotal FROM TransactionDetails WHERE TransactionID = @TransactionID";
                SqlCommand cmdDetails = new SqlCommand(queryDetails, conn);
                cmdDetails.Parameters.AddWithValue("@TransactionID", transactionID);
                SqlDataAdapter da = new SqlDataAdapter(cmdDetails);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                gvTransactionDetails.DataSource = dt;
                gvTransactionDetails.DataBind();
            }
        }
    }
}
