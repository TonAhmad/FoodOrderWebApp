using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Project2.Models
{
    public class Report
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["Set"].ConnectionString;


        // Method untuk mengambil data transaksi
        public DataTable GetTransactionReports()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT t.transID, o.orderID, o.customerName, t.total, t.transDate 
                    FROM Transactions.TransHeader t
                    JOIN orders.OrderHeader o ON t.orderID = o.orderID
                    ORDER BY t.transDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // Method untuk mendapatkan laporan penjualan produk
        public DataTable GetSalesReport(string filterType, int filterValue)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT p.productName, SUM(d.quantity) AS TotalSold, SUM(d.subtotal) AS TotalEarnings
                    FROM Transactions.TransDetail d
                    JOIN item.product p ON d.productID = p.productID
                    JOIN Transactions.TransHeader t ON d.transID = t.transID ";

                // Filter berdasarkan hari atau bulan
                if (filterType == "day")
                {
                    query += " WHERE DAY(t.transDate) = @FilterValue AND MONTH(t.transDate) = MONTH(GETDATE()) ";
                }
                else if (filterType == "month")
                {
                    query += " WHERE MONTH(t.transDate) = @FilterValue ";
                }

                query += " GROUP BY p.productName ORDER BY TotalSold DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FilterValue", filterValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // Method untuk mendapatkan total pendapatan
        public decimal GetTotalEarnings()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT SUM(total) FROM Transactions.TransHeader";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    con.Close();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }
    }
}