using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2._Cust
{
    public partial class SuccessOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["orderID"] != null)
                {
                    Literal1.Text = Session["orderID"].ToString();
                }

                if (Session["customerName"] != null)
                {
                    litCustomerName.Text = Session["customerName"].ToString();
                    Literal2.Text = Session["customerName"].ToString();
                }

                if (Session["cart"] != null)
                {
                    List<Checkout.CartItem> cart = (List<Checkout.CartItem>)Session["cart"];
                    decimal totalPrice = 0;
                    string orderDetails = "";

                    for (int i = 0; i < cart.Count; i++)
                    {
                        var item = cart[i];
                        decimal itemTotal = item.price * item.quantity;
                        totalPrice += itemTotal;

                        // Format data agar sesuai dengan desain tanpa tabel
                        orderDetails += $"<div class='order-item'>" +
                                        $"<span class='item-name'>{item.name}</span>" +
                                        $"<span class='item-qty'>{item.quantity}</span>" +
                                        $"<span class='item-price'>Rp {itemTotal:N0}</span>" +
                                        $"</div>";
                    }

                    litOrderDetails.Text = orderDetails;
                    litTotalPrice.Text = $"Rp {totalPrice:N0}";
                }
                else
                {
                    litOrderDetails.Text = "<div class='text-center'>Tidak ada pesanan.</div>";
                    litTotalPrice.Text = "Rp 0";
                }
            }

        }
    }
}