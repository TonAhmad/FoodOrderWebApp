using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Project2.Models
{
    public class AddAdmin
    {
        public string username, fullname, email, password_hash, phone_number, address, role;
        private readonly SqlConnection con = new SqlConnection(Koneksi.connString);
        private string flag;
        public DataSet ds = new DataSet();

        // Hash Password menggunakan SHA-256
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // Validasi Input
        private bool ValidateInput(out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password_hash))
            {
                errorMessage = "Username, email, dan password tidak boleh kosong.";
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorMessage = "Format email tidak valid.";
                return false;
            }

            if (password_hash.Length < 6)
            {
                errorMessage = "Password minimal 6 karakter.";
                return false;
            }

            if (role.ToLower() != "admin" && role.ToLower() != "cashier")
            {
                errorMessage = "Role hanya bisa 'admin' atau 'cashier'.";
                return false;
            }

            return true;
        }

        private bool IsUserExists()
        {
            string query = "SELECT COUNT(*) FROM Adm.Admin WHERE username = @username OR email = @email";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();
                return count > 0;
            }
        }

        public string Create()
        {
            if (!ValidateInput(out string errorMsg))
            {
                return errorMsg;
            }

            if (IsUserExists())
            {
                return "Username atau email sudah terdaftar.";
            }

            string hashedPassword = HashPassword(password_hash);
            try
            {
                con.Open();
                string query = "INSERT INTO Adm.Admin (username, fullname, email, password_hash, phone_number, address, role) " +
                               "VALUES (@username, @fullname, @email, @pw, @phone, @address, @role)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pw", hashedPassword);
                    cmd.Parameters.AddWithValue("@phone", phone_number);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@role", role.ToLower());

                    flag = cmd.ExecuteNonQuery() > 0 ? "success" : "failed";
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
            return flag;
        }

        public string Update()
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return "Username tidak boleh kosong.";
            }

            try
            {
                con.Open();
                string query = "UPDATE Adm.Admin SET fullname = @fullname, email = @email, phone_number = @phone, address = @address WHERE username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone_number);
                    cmd.Parameters.AddWithValue("@address", address);

                    flag = cmd.ExecuteNonQuery() > 0 ? "success" : "failed";
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
            return flag;
        }

        public string Delete()
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return "Username tidak boleh kosong.";
            }

            try
            {
                con.Open();
                string query = "DELETE FROM Adm.Admin WHERE username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    flag = cmd.ExecuteNonQuery() > 0 ? "success" : "failed";
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
            return flag;
        }

        public DataSet Read()
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM Adm.Admin";
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.Fill(ds, "Admin");
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
        
        public ArrayList Search()
        {
            ArrayList data = new ArrayList();
            SqlDataReader dr = null;
            try
            {
                con.ConnectionString = Koneksi.connString;
                con.Open();
                string query = "SELECT * FROM adm.Admin WHERE username = @username";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@username", username);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    data.Add(dr[0].ToString());
                    data.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr = null;
            }
            finally
            {
                con.Close();
            }
            return data;
        }
    }
}