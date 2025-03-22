using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class ProductTopSeller
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public int TotalSold { get; set; }

        public static List<ProductTopSeller> GetTopSellers()
        {
            List<ProductTopSeller> topSellers = new List<ProductTopSeller>();
            

            using (SqlConnection con = new SqlConnection(Koneksi.connString))
            {
                string query = @"
                SELECT TOP 3 
                    p.productID, 
                    p.productName, 
                    p.imagePath, 
                    SUM(td.quantity) AS totalSold
                FROM Transactions.TransDetail td
                JOIN item.Product p ON td.productID = p.productID
                GROUP BY p.productID, p.productName, p.imagePath
                ORDER BY totalSold DESC;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        topSellers.Add(new ProductTopSeller
                        {
                            ProductID = reader["productID"].ToString(),
                            ProductName = reader["productName"].ToString(),
                            ImagePath = reader["imagePath"].ToString(),
                            TotalSold = Convert.ToInt32(reader["totalSold"])
                        });
                    }
                }
            }
            return topSellers;
        }
    }
}