using System;
using System.Web.UI;

namespace Project2._Cashier
{
    public partial class ProcessPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ambil total dari session atau database (sementara diset manual)
                decimal totalAmount = 0;
                if (Session["TotalAmount"] != null)
                {
                    decimal.TryParse(Session["TotalAmount"].ToString(), out totalAmount);
                }
                lblTotalAmount.Text = $"Rp {totalAmount:N0}";
            }
        }

        protected void txtAmountPaid_TextChanged(object sender, EventArgs e)
        {
            decimal totalAmount = Session["TotalAmount"] != null ? (decimal)Session["TotalAmount"] : 0;
            decimal amountPaid;

            if (decimal.TryParse(txtAmountPaid.Text, out amountPaid))
            {
                decimal change = amountPaid - totalAmount;
                lblChange.Text = change >= 0 ? $"Rp {change:N0}" : "Insufficient Payment";
            }
            else
            {
                lblChange.Text = "Invalid Amount";
            }
        }

        protected void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            decimal totalAmount = Session["TotalAmount"] != null ? (decimal)Session["TotalAmount"] : 0;
            decimal amountPaid;

            if (decimal.TryParse(txtAmountPaid.Text, out amountPaid) && amountPaid >= totalAmount)
            {
                // Simpan transaksi ke database di sini

                // Redirect ke halaman sukses
                Response.Redirect("PaymentSuccess.aspx");
            }
            else
            {
                Response.Write("<script>alert('Insufficient payment!');</script>");
            }
        }
    }
}