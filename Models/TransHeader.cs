using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections;
using System.Diagnostics;

namespace Project2.Models
{
    public class TransHeader
    {
        public string TransID { get; set; }
        public string OrderID { get; set; }
        public string AdminID { get; set; }
        public decimal Total { get; set; }

        readonly SqlConnection con = new SqlConnection(Koneksi.connString);

        public TransHeader(string transID, string orderID, string adminID, decimal total)
        {
            TransID = transID;
            OrderID = orderID;
            AdminID = adminID;
            Total = total;
        }

        public string Create()
        {
            try
            {
                con.Open();
                string query = "INSERT INTO Transactions.TransHeader (transID, orderID, admin_ID, total) VALUES (@transID, @orderID, @adminID, @total)";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@transID", TransID);
                    com.Parameters.AddWithValue("@orderID", OrderID);
                    com.Parameters.AddWithValue("@adminID", AdminID);
                    com.Parameters.AddWithValue("@total", Total);
                    int rows = com.ExecuteNonQuery();
                    if (rows > 0)
                        return "success";
                    else
                        return "failed: no rows affected";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}