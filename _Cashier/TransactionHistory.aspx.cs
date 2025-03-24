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
                string query = "SELECT orderID, cashier_id, orderStatus, orderDate FROM orders.OrderHeader ORDER BY orderDate DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvTransactions.DataSource = dt;
                gvTransactions.DataBind();
            }
        }

        protected void gvTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                string orderID = e.CommandArgument.ToString();
                Response.Redirect("TransactionDetails.aspx?orderID=" + orderID);
            }
        }
    }
}
