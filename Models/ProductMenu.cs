using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class ProductMenu
    {
        public string productID;
        public string productName;
        public string categoryName;
        public decimal price;
        public int stock;
        public string imagePath;

        readonly SqlConnection con = new SqlConnection(Koneksi.connString);
        string flag;
        public DataSet ds = new DataSet();

        public DataTable GetCategories()
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string query = "SELECT categoryID, categoryName FROM item.Category";
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

        public DataTable GetProductsWithCategoryName()
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string query = @"
                SELECT p.productID, p.productName, c.categoryID, c.categoryName, p.price, p.stock, p.imagePath
                FROM item.Product p
                JOIN item.Category c ON p.categoryID = c.categoryID
                ORDER BY c.categoryID";  // Urutkan berdasarkan kategori
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
    }
}