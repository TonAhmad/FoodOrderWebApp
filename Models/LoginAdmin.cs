using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Project2.Models
{
    public class LoginAdmin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; private set; }
        public string Fullname { get; private set; }

        readonly string connStr = ConfigurationManager.ConnectionStrings["set"].ConnectionString;

        public bool Authenticate()
        {
            bool isAuthenticated = false;
            string storedHash = string.Empty;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                try
                {
                    con.Open();
                    string query = "SELECT fullname, role, password_hash FROM adm.Admin WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", Username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Fullname = reader["fullname"].ToString();
                                Role = reader["role"].ToString();
                                storedHash = reader["password_hash"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            // Verifikasi password yang dimasukkan dengan hash di database
            if (!string.IsNullOrEmpty(storedHash) && VerifyPassword(Password, storedHash))
            {
                isAuthenticated = true;
            }

            return isAuthenticated;
        }

        // Fungsi untuk meng-hash password (gunakan saat pendaftaran user baru)
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Fungsi untuk memverifikasi password
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == storedHash;
        }
    }
}