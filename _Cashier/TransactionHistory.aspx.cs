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

        private void LoadTransactionHistory(string searchQuery = "")
        {
            string connString = ConfigurationManager.ConnectionStrings["Set"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT orderID, orderDate FROM orders.OrderHeader ORDER BY orderDate DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Jika ada pencarian, filter berdasarkan orderID
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = $"orderID LIKE '%{searchQuery}%'";
                    dt = dv.ToTable();
                }

                gvTransactions.DataSource = dt;
                gvTransactions.DataBind();

                // Update halaman label paging
                lblPageNumberTransaction.Text = $"Page {gvTransactions.PageIndex + 1} of {gvTransactions.PageCount}";

                // Nonaktifkan tombol jika di awal/akhir halaman
                btnPrevPageTransaction.Enabled = gvTransactions.PageIndex > 0;
                btnNextPageTransaction.Enabled = gvTransactions.PageIndex < gvTransactions.PageCount - 1;
            }
        }

        // Event Paging GridView
        protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransactions.PageIndex = e.NewPageIndex;
            LoadTransactionHistory(txtSearchTransaction.Text); // Load ulang dengan search query jika ada
        }

        // Event Search Button
        protected void btnSearchTransaction_Click(object sender, EventArgs e)
        {
            LoadTransactionHistory(txtSearchTransaction.Text);
        }

        // Event Reset Button
        protected void btnResetTransaction_Click(object sender, EventArgs e)
        {
            txtSearchTransaction.Text = "";
            LoadTransactionHistory(); // Kembali ke data awal tanpa filter
        }

        // Event untuk tombol Previous Page
        protected void btnPrevPageTransaction_Click(object sender, EventArgs e)
        {
            if (gvTransactions.PageIndex > 0)
            {
                gvTransactions.PageIndex--;
                LoadTransactionHistory(txtSearchTransaction.Text);
            }
        }

        // Event untuk tombol Next Page
        protected void btnNextPageTransaction_Click(object sender, EventArgs e)
        {
            if (gvTransactions.PageIndex < gvTransactions.PageCount - 1)
            {
                gvTransactions.PageIndex++;
                LoadTransactionHistory(txtSearchTransaction.Text);
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
