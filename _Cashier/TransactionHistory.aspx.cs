using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Project2._Cashier
{
    public partial class TransactionHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTransactionHistory();
            }
        }

        private void LoadTransactionHistory()
        {
            string connString = ConfigurationManager.ConnectionStrings["Set"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT transID, admin_id, cashier_id, total FROM Transactions.TransHeader ORDER BY transDate DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                da.Fill(dt);
                conn.Close();

                gvTransactions.DataSource = dt;
                gvTransactions.DataBind();
            }
        }

        protected void gvTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                string transactionID = e.CommandArgument.ToString();
                Response.Redirect($"TransactionDetails.aspx?TransactionID={transactionID}");
            }
        }
    }
}
