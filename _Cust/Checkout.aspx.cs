using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2.Models;
using Newtonsoft.Json;

namespace Project2._Cust
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["saveCart"] == "1" && Request.HttpMethod == "POST")
            {
                using (var reader = new StreamReader(Request.InputStream))
                {
                    string json = reader.ReadToEnd();
                    Session["cart"] = JsonConvert.DeserializeObject<List<CartItem>>(json);
                }
                Response.End();
            }
        }
        private string GenerateOrderID()
        {
            return "ORD" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public class CartItem
        {
            public string productID { get; set; } // Pastikan ada productID
            public string name { get; set; }
            public decimal price { get; set; }
            public int quantity { get; set; }
        }

        protected void btnConfirmCheckout_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text.Trim();

            if (string.IsNullOrEmpty(customerName))
            {
                Response.Write("<script>alert('Nama tidak boleh kosong!');</script>");
                return;
            }

            // Ambil data cart dari session
            if (Session["cart"] == null)
            {
                Response.Write("<script>alert('Cart kosong! Silakan tambahkan produk.');</script>");
                return;
            }

            List<CartItem> cart = (List<CartItem>)Session["cart"];

            if (cart.Count == 0)
            {
                Response.Write("<script>alert('Cart kosong!');</script>");
                return;
            }

            // Generate OrderID
            string orderID = GenerateOrderID();

            // Buat instance OrderHeader dan simpan
            OrderHeader order = new OrderHeader();
            order.orderID = orderID;
            order.customerName = customerName;
            order.orderStatus = "pending";

            string orderResult = order.Create();

            if (orderResult != "success")
            {
                Response.Write("<script>alert('Terjadi kesalahan: " + orderResult + "');</script>");
                return;
            }

            // Insert ke OrderDetails
            bool allSuccess = true;
            foreach (CartItem item in cart)
            {
                OrderDetails orderDetail = new OrderDetails();
                orderDetail.orderID = orderID;
                orderDetail.productID = item.productID;
                orderDetail.quantity = item.quantity;
                orderDetail.subtotal = item.price * item.quantity;

                string detailResult = orderDetail.Create();
                if (detailResult != "success")
                {
                    allSuccess = false;
                    Response.Write("<script>alert('Terjadi kesalahan pada item " + item.name + ": " + detailResult + "');</script>");
                    break;
                }
            }

            if (allSuccess)
            {
                if (allSuccess)
                {
                    // Simpan informasi order untuk halaman SuccessOrder
                    Session["orderID"] = orderID;
                    Session["customerName"] = customerName;
                    Session["cartSummary"] = cart; 

                    // Redirect ke halaman SuccessOrder
                    Response.Redirect("SuccessOrder.aspx");
                }

            }
        }
    }
}