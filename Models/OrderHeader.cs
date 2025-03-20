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
        public string adminID = null; // Ubah default menjadi NULL
        public string orderStatus; // Default pending

        readonly SqlConnection con = new SqlConnection(Koneksi.connString);
        string flag;

        public string Create()
        {
            try
            {
                con.Open();

                if (string.IsNullOrEmpty(customerName))
                {
                    return "Customer name cannot be empty!";
                }

                string query = "INSERT INTO orders.OrderHeader (orderID, customerName, admin_id, orderStatus) " +
                               "VALUES (@orderID, @customerName, @adminID, @orderStatus)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@orderID", orderID);
                cmd.Parameters.AddWithValue("@customerName", customerName);
                cmd.Parameters.AddWithValue("@adminID", (object)adminID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@orderStatus", orderStatus);

                flag = cmd.ExecuteNonQuery() > 0 ? "success" : "failed";
            }
            catch (Exception ex)
            {
                flag = "Database Error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }
    }
}