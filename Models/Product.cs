using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class Product
    {
        public string productID;
        public string productName;
        public string categoryID;
        public decimal price;
        public int stock;
        public string imagePath;

        readonly SqlConnection con = new SqlConnection(Koneksi.connString);
        string flag;
        public DataSet ds = new DataSet();

        public string Create()
        {
            try
            {
                con.Open();
                string query = "INSERT INTO item.product (productName, categoryID, price, stock, imagePath) VALUES (@name, @category, @price, @stock, @image)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", productName);
                cmd.Parameters.AddWithValue("@category", categoryID);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@stock", stock);

                // Perbaikan untuk imagePath agar tidak null
                cmd.Parameters.AddWithValue("@image", string.IsNullOrEmpty(imagePath) ? (object)DBNull.Value : imagePath);

                int rowsAffected = cmd.ExecuteNonQuery();
                flag = rowsAffected > 0 ? "success" : "failed";

                // Debugging untuk memastikan imagePath dikirim ke database
                Console.WriteLine("Image Path: " + imagePath);
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


        public DataSet Read()
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM item.Product";
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.Fill(ds, "Category");
                }
            }
            catch (Exception ex)
            {
                flag = "Error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        public string Update()
        {
            try
            {
                con.Open();
                string query = "UPDATE item.Product SET productName = @name, categoryID = @category, price = @price, stock = @stock, imagePath = @image WHERE productID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", productID);
                cmd.Parameters.AddWithValue("@name", productName);
                cmd.Parameters.AddWithValue("@category", categoryID);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@stock", stock);
                cmd.Parameters.AddWithValue("@image", imagePath);
                flag = cmd.ExecuteNonQuery() > 0 ? "success" : "failed";
            }
            catch (Exception ex)
            {
                flag = "Error" + ex.Message;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

        public string Delete()
        {
            try
            {
                con.Open();
                string query = "DELETE FROM item.Product WHERE productID = @id";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@id", categoryID);
                flag = com.ExecuteNonQuery() > 0 ? "success" : "failed";
            }
            catch (Exception ex)
            {
                flag = "error" + ex.Message;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }
    }
}