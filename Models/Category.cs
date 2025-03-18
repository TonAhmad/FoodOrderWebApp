using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class Category
    {
        public string categoryID, categoryName;
        readonly SqlConnection con = new SqlConnection(Koneksi.connString);
        string flag;
        public DataSet ds = new DataSet();

        public string Create()
        {
            try
            {
                con.Open();
                string query = "INSERT INTO Item.Category(categoryName) VALUES (@nama)";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@nama", categoryName);
                flag = com.ExecuteNonQuery() > 0 ? "success" : "failed";
            }
            catch(Exception ex)
            {
                flag = "Error" + ex.Message;
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
                string query = "SELECT * FROM item.Category";
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
                string query = "UPDATE Item.Category SET categoryName = @nama WHERE categoryID = @id";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@nama", categoryName);
                com.Parameters.AddWithValue("@id", categoryID);
                flag = com.ExecuteNonQuery() > 0 ? "success" : "failed";
            }
            catch(Exception ex)
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
                string query = "DELETE FROM Item.Category WHERE categoryID = @id";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@id", categoryID);
                flag = com.ExecuteNonQuery() > 0 ? "success" : "failed";
            }
            catch(Exception ex)
            {
                flag = "error" + ex.Message;
            }
            finally
            { 
                con.Close();
            }
            return flag;
        }

        public DataTable GetCategories()
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string query = "SELECT categoryID, categoryName FROM item.category";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Logging atau handle error jika perlu
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