using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class OrderDetails
    {
        public string orderID;
        public string productID;
        public int quantity;
        public decimal subtotal;

        readonly SqlConnection con = new SqlConnection(Koneksi.connString);
        string flag;

        public string Create()
        {
            try
            {
                con.Open();

                // Validasi quantity tidak boleh kurang dari 1
                if (quantity <= 0)
                {
                    return "Quantity must be greater than 0!";
                }

                string query = "INSERT INTO orders.OrderDetail (orderID, productID, quantity, subtotal) " +
                               "VALUES (@orderID, @productID, @quantity, @subtotal)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@orderID", orderID);
                cmd.Parameters.AddWithValue("@productID", productID);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@subtotal", subtotal);

                flag = cmd.ExecuteNonQuery() > 0 ? "success" : "failed";
            }
            catch (Exception ex)
            {
                flag = "Error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }
    }
}