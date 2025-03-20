using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

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
            SELECT oh.orderID, oh.customerName, p.productName, od.quantity, od.subtotal
            FROM orders.OrderHeader oh
            JOIN orders.OrderDetail od ON oh.orderID = od.orderID
            JOIN item.Product p ON od.productID = p.productID
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

        public bool ProcessTransaction(string orderID, string adminID)
        {
            using (SqlConnection con = new SqlConnection(Koneksi.connString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(); // Gunakan transaksi SQL

                try
                {
                    // 1. Ambil total harga dari OrderHeader
                    decimal total = 0;
                    string queryTotal = "SELECT total FROM Orders.OrderHeader WHERE orderID = @orderID";
                    using (SqlCommand cmd = new SqlCommand(queryTotal, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@orderID", orderID);
                        total = (decimal)cmd.ExecuteScalar();
                    }

                    // 2. Buat transID baru
                    string transID = GenerateTransactionID(con, transaction, adminID);



                    // 3. Insert ke TransHeader
                    string queryTransHeader = @"
                INSERT INTO Transactions.TransHeader (transID, orderID, admin_ID, total) 
                VALUES (@transID, @orderID, @adminID, @total)";
                    using (SqlCommand cmd = new SqlCommand(queryTransHeader, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@transID", transID);
                        cmd.Parameters.AddWithValue("@orderID", orderID);
                        cmd.Parameters.AddWithValue("@adminID", adminID);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.ExecuteNonQuery();
                    }

                    // 4. Salin data dari OrderDetail ke TransDetail
                    string queryTransDetail = @"
                INSERT INTO Transactions.TransDetail (transID, productID, quantity, subtotal)
                SELECT @transID, productID, quantity, subtotal 
                FROM Orders.OrderDetail WHERE orderID = @orderID";
                    using (SqlCommand cmd = new SqlCommand(queryTransDetail, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@transID", transID);
                        cmd.Parameters.AddWithValue("@orderID", orderID);
                        cmd.ExecuteNonQuery();
                    }

                    // 5. Hapus data dari OrderHeader dan OrderDetail setelah diproses
                    string deleteOrderDetail = "DELETE FROM Orders.OrderDetail WHERE orderID = @orderID";
                    using (SqlCommand cmd = new SqlCommand(deleteOrderDetail, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@orderID", orderID);
                        cmd.ExecuteNonQuery();
                    }

                    string deleteOrderHeader = "DELETE FROM Orders.OrderHeader WHERE orderID = @orderID";
                    using (SqlCommand cmd = new SqlCommand(deleteOrderHeader, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@orderID", orderID);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit(); // Simpan transaksi ke database
                    return true;
                }
                catch
                {
                    transaction.Rollback(); // Batalkan jika ada error
                    return false;
                }
            }
        }

        // Fungsi untuk membuat transID baru (format: TRX001, TRX002, dst.)
        public string GenerateTransactionID(SqlConnection con, SqlTransaction transaction, string adminID)
        {
            // Ambil 3 digit tengah dari adminID, contoh: ADM001 → M001
            string adminPart = adminID.Length >= 5 ? adminID.Substring(1, 4) : adminID;

            // Format waktu: TahunBulanTanggalJamMenit
            string dateTimePart = DateTime.Now.ToString("yyyyMMddHHmm");

            // Nomor random dari 10 - 150
            Random rnd = new Random();
            int randomNumber = rnd.Next(10, 151);

            // Query untuk mengecek jumlah transaksi
            string query = "SELECT COUNT(*) FROM Transactions.TransHeader";
            int count = 0;

            using (SqlCommand cmd = new SqlCommand(query, con, transaction))
            {
                count = (int)cmd.ExecuteScalar() + 1;
            }

            // Gabungkan semuanya
            return $"{adminPart}{dateTimePart}{randomNumber:D3}";
        }
    }
}