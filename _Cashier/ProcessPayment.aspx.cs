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
                CalculateTotalAmount();
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

        private void CalculateTotalAmount()
        {
            decimal totalAmount = 0;

            foreach (RepeaterItem item in rptSelectedOrder.Items)
            {
                Label lblSubtotal = (Label)item.FindControl("lblSubtotal");

                if (lblSubtotal != null && !string.IsNullOrEmpty(lblSubtotal.Text))
                {
                    // Hapus "Rp" dan konversi ke decimal
                    string subtotalText = lblSubtotal.Text.Replace("Rp", "").Trim();
                    totalAmount += Convert.ToDecimal(subtotalText);
                }
            }

            // Tampilkan total di Label
            lblTotalAmount.Text = "Rp " + totalAmount.ToString("N0");

            // Simpan total di ViewState untuk digunakan di pembayaran
            ViewState["TotalAmount"] = totalAmount;
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

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            // Ambil orderID dari tombol yang diklik
            Button btn = (Button)sender;
            string orderID = btn.CommandArgument;

            // Query untuk mendapatkan data pesanan berdasarkan orderID
            string query = @"
            SELECT oh.orderID, oh.customerName, p.productID, p.productName, od.quantity, (od.quantity * p.price) AS subtotal
            FROM Orders.OrderDetail od
            JOIN Orders.OrderHeader oh ON od.orderID = oh.orderID
            JOIN item.Product p ON od.productID = p.productID
            WHERE od.orderID = @orderID";



            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(Koneksi.connString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@orderID", orderID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            // Bind data ke Repeater untuk menampilkan di tabel "Selected Order Details"
            rptSelectedOrder.DataSource = dt;
            rptSelectedOrder.DataBind();

            CalculateTotalAmount();
        }



        protected void btnPay_Click(object sender, EventArgs e)
        {
            decimal totalAmount = Convert.ToDecimal(ViewState["TotalAmount"]); // Ambil total dari ViewState
            decimal amountPaid;

            // Validasi input Amount Paid
            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out amountPaid))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Masukkan nominal pembayaran!');", true);
                return;
            }

            if (amountPaid < totalAmount)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Nominal kurang dari total yang harus dibayar!');", true);
                return;
            }

            // Hitung kembalian jika lebih bayar
            decimal change = amountPaid - totalAmount;
            txtChange.Text = "Rp " + change.ToString("N0");

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Nominal terpenuhi, lanjutkan transaksi!');", true);
        }

        protected void btnCompleteTransaction_Click(object sender, EventArgs e)
        {
            decimal totalAmount = Convert.ToDecimal(ViewState["TotalAmount"]);
            decimal amountPaid;

            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out amountPaid))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Masukkan nominal pembayaran!');", true);
                return;
            }

            if (amountPaid < totalAmount)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Nominal kurang dari total yang harus dibayar!');", true);
                return;
            }

            // ✅ Ambil orderID dari repeater
            string orderID = null;
            if (rptSelectedOrder.Items.Count > 0)
            {
                Label lblOrderID = (Label)rptSelectedOrder.Items[0].FindControl("lblOrderID");
                orderID = lblOrderID.Text;
            }

            if (string.IsNullOrEmpty(orderID))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error: Order ID tidak ditemukan!');", true);
                return;
            }

            string adminID = Session["admin_id"] as string;

            // ✅ Siapkan DataTable untuk `orderDetails`
            DataTable orderDetails = new DataTable();
            orderDetails.Columns.Add("productID", typeof(string));
            orderDetails.Columns.Add("quantity", typeof(int));
            orderDetails.Columns.Add("subtotal", typeof(decimal));

            foreach (RepeaterItem item in rptSelectedOrder.Items)
            {
                Label lblProductID = (Label)item.FindControl("lblProductID");
                Label lblQuantity = (Label)item.FindControl("lblQuantity");
                Label lblSubtotal = (Label)item.FindControl("lblSubtotal");

                string productID = lblProductID.Text;
                int quantity = Convert.ToInt32(lblQuantity.Text);
                decimal subtotal = Convert.ToDecimal(lblSubtotal.Text.Replace("Rp ", "").Replace(",", ""));

                orderDetails.Rows.Add(productID, quantity, subtotal);
            }

            // 🔥 Jalankan transaksi
            string result = orderCashier.ProcessTransaction(orderID, adminID, totalAmount, orderDetails);

            if (result.StartsWith("error"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error: " + result + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Transaksi berhasil!'); window.location='ProcessPayment.aspx';", true);
                LoadPendingOrders();
                LoadConfirmedOrders();
            }
        }


        private void LoadConfirmedOrders()
        {
            OrderCashier orderCashier = new OrderCashier();
            rptConfirmedOrders.DataSource = orderCashier.GetConfirmedOrders();
            rptConfirmedOrders.DataBind();
        }
    }
}