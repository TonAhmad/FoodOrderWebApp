using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web.UI;
using Project2.Models;
using System.Web.UI.WebControls;

namespace Project2._Cashier
{
    public partial class ProcessPayment : System.Web.UI.Page
    {
        OrderCashier orderCashier = new OrderCashier();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPendingOrders();
                LoadConfirmedOrders();
            }
        }

        private void LoadPendingOrders()
        {
            DataTable dt = orderCashier.GetPendingOrders();
            if (dt.Rows.Count > 0)
            {
                rptPendingOrders.DataSource = dt;
                rptPendingOrders.DataBind();
            }
            else
            {
                rptPendingOrders.DataSource = null;
                rptPendingOrders.DataBind();
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            // Contoh perhitungan kembalian
            decimal total = 0;
            decimal amountPaid = 0;

            if (!decimal.TryParse(txtAmount.Text, out amountPaid))
            {
                lblMessage.Text = "Invalid amount entered.";
                lblMessage.CssClass = "error-message";
                return;
            }

            // Misalnya total transaksi diambil dari session atau hidden field
            if (Session["totalPayment"] != null)
            {
                total = Convert.ToDecimal(Session["totalPayment"]);
            }

            // Hitung kembalian
            decimal change = amountPaid - total;
            txtChange.Text = change >= 0 ? change.ToString("F2") : "0.00";
        }


        // Event handler untuk tombol "Confirm"
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string orderID = btn.CommandArgument;

            // Ambil admin_id dari Session
            string adminID = Session["admin_id"] as string;

            if (!string.IsNullOrEmpty(adminID))
            {
                bool success = orderCashier.ConfirmOrder(orderID, adminID);
                if (success)
                {
                    lblMessage.Text = "Order " + orderID + " berhasil dikonfirmasi.";
                    lblMessage.CssClass = "success-message";
                    LoadPendingOrders(); // Refresh daftar pesanan
                    LoadConfirmedOrders();
                }
                else
                {
                    lblMessage.Text = "Gagal mengonfirmasi order.";
                    lblMessage.CssClass = "error-message";
                }
            }
            else
            {
                lblMessage.Text = "Gagal mengonfirmasi order. Silakan login ulang.";
                lblMessage.CssClass = "error-message";
            }
        }

        protected void btnCompleteTransaction_Click(object sender, EventArgs e)
        {
            // Ambil adminID dari session
            string adminID = Session["admin_id"]?.ToString() ?? "ADM001"; // Default jika session kosong

            using (SqlConnection con = new SqlConnection(Koneksi.connString))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        OrderCashier orderCashier = new OrderCashier();
                        string transID = orderCashier.GenerateTransactionID(con, transaction, adminID);

                        // Lanjutkan proses transaksi...
                        lblMessage.Text = "Transaction Completed! ID: " + transID;
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        lblMessage.Text = "Error: " + ex.Message;
                    }
                }
            }
        }


        private void LoadConfirmedOrders()
        {
            DataTable dt = orderCashier.GetConfirmedOrders();
            rptConfirmedOrders.DataSource = dt;
            rptConfirmedOrders.DataBind();
        }
    }
}