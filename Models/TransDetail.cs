using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class TransDetail
    {
        public string TransID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }

        readonly SqlConnection con = new SqlConnection(Koneksi.connString);

        public TransDetail(string transID, string productID, int quantity, decimal subtotal)
        {
            TransID = transID;
            ProductID = productID;
            Quantity = quantity;
            Subtotal = subtotal;
        }

        public string Create()
        {
            try
            {
                con.Open();
                string query = "INSERT INTO Transactions.TransDetail (transID, productID, quantity, subtotal) VALUES (@transID, @productID, @quantity, @subtotal)";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@transID", TransID);
                    com.Parameters.AddWithValue("@productID", ProductID);
                    com.Parameters.AddWithValue("@quantity", Quantity);
                    com.Parameters.AddWithValue("@subtotal", Subtotal);
                    return com.ExecuteNonQuery() > 0 ? "success" : "failed";
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