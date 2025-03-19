using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class OrderHeader
    {
        public string orderID;
        public string customerName;
        public string adminID = ""; // Default kosong, karena akan diisi oleh sesi cashier
        public string orderStatus;

        readonly SqlConnection con = new SqlConnection(Koneksi.connString);
        string flag;

        public string Create()
        {
            try
            {
                con.Open();

                // Validasi customerName tidak boleh kosong
                if (string.IsNullOrEmpty(customerName))
                {
                    return "Customer name cannot be empty!";
                }

                // Validasi orderStatus harus sesuai dengan yang diizinkan
                if (orderStatus != "pending" && orderStatus != "confirmed" && orderStatus != "completed" && orderStatus != "canceled")
                {
                    return "Invalid order status!";
                }

                // Pastikan adminID tidak null, karena akan diisi oleh sesi cashier nanti
                if (string.IsNullOrEmpty(adminID))
                {
                    adminID = ""; // Default kosong
                }

                string query = "INSERT INTO orders.OrderHeader (orderID, customerName, admin_id, orderStatus) " +
                               "VALUES (@orderID, @customerName, @adminID, @orderStatus)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@orderID", orderID);
                cmd.Parameters.AddWithValue("@customerName", customerName);
                cmd.Parameters.AddWithValue("@adminID", adminID); // Tidak boleh null, minimal string kosong
                cmd.Parameters.AddWithValue("@orderStatus", orderStatus);

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