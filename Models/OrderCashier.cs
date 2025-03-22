using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Project2.Models
{
    public class OrderCashier
    {

        readonly SqlConnection con = new SqlConnection(Koneksi.connString);
        public DataSet ds = new DataSet();

        // Fungsi untuk mengambil semua pesanan dengan status 'pending'
        public DataTable GetPendingOrders()
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string query = "SELECT orderID, customerName, orderDate, orderStatus FROM orders.OrderHeader WHERE orderStatus = 'pending'";
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        // Fungsi untuk mengonfirmasi pesanan (mengubah status menjadi 'confirmed')
        public bool ConfirmOrder(string orderID, string adminID)
        {
            try
            {
                con.Open();
                string query = "UPDATE orders.OrderHeader SET orderStatus = 'confirmed', admin_id = @adminID WHERE orderID = @orderID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@orderID", orderID);
                    cmd.Parameters.AddWithValue("@adminID", adminID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Mengembalikan true jika ada baris yang diubah
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable GetConfirmedOrders()
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string query = @"
        SELECT oh.orderID, oh.customerName, oh.orderDate, oh.orderStatus
        FROM orders.OrderHeader oh
        WHERE oh.orderStatus = 'confirmed'
        ORDER BY oh.orderDate ASC";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public string ProcessTransaction(string orderID, string adminID, decimal totalAmount, DataTable orderDetails)
        {
            string transID = GenerateTransID(adminID);
            SqlTransaction transaction = null;

            try
            {
                con.Open();
                transaction = con.BeginTransaction();

                // 1️⃣ Insert ke TransHeader
                string queryHeader = "INSERT INTO Transactions.TransHeader (transID, orderID, admin_ID, total) VALUES (@transID, @orderID, @adminID, @total)";
                using (SqlCommand cmdHeader = new SqlCommand(queryHeader, con, transaction))
                {
                    cmdHeader.Parameters.AddWithValue("@transID", transID);
                    cmdHeader.Parameters.AddWithValue("@orderID", orderID);
                    cmdHeader.Parameters.AddWithValue("@adminID", adminID);
                    cmdHeader.Parameters.AddWithValue("@total", totalAmount);
                    cmdHeader.ExecuteNonQuery();
                }

                // 2️⃣ Insert ke TransDetail (Looping dari `orderDetails`)
                foreach (DataRow row in orderDetails.Rows)
                {
                    string queryDetail = "INSERT INTO Transactions.TransDetail (transID, productID, quantity, subtotal) VALUES (@transID, @productID, @quantity, @subtotal)";
                    using (SqlCommand cmdDetail = new SqlCommand(queryDetail, con, transaction))
                    {
                        cmdDetail.Parameters.AddWithValue("@transID", transID);
                        cmdDetail.Parameters.AddWithValue("@productID", row["productID"].ToString());
                        cmdDetail.Parameters.AddWithValue("@quantity", Convert.ToInt32(row["quantity"]));
                        cmdDetail.Parameters.AddWithValue("@subtotal", Convert.ToDecimal(row["subtotal"]));

                        int rowsAffected = cmdDetail.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Insert ke TransDetail gagal.");
                        }
                    }
                }

                // 3️⃣ Update status pesanan menjadi "Completed"
                string queryUpdate = "UPDATE Orders.OrderHeader SET orderStatus = 'Completed' WHERE orderID = @orderID";
                using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, con, transaction))
                {
                    cmdUpdate.Parameters.AddWithValue("@orderID", orderID);
                    cmdUpdate.ExecuteNonQuery();
                }

                // ✅ Semua sukses, commit transaksi
                transaction.Commit();
                return transID;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback(); // ❌ Jika ada yang gagal, rollback semua
                }
                return "error: " + ex.Message; // Indikasi error agar bisa ditampilkan
            }
            finally
            {
                con.Close();
            }
        }


        public string GenerateTransID(string adminID)
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(10, 1000);
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"{adminID}{timestamp}{randomNum}";
        }
    }
}