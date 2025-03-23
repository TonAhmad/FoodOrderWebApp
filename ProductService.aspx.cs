using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2
{
    public partial class ProductService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static int GetProductStock(string productID)
        {
            int stock = 0;
            string connString = ConfigurationManager.ConnectionStrings["Set"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "SELECT stock FROM item.Product WHERE productID = @productID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@productID", productID);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        stock = Convert.ToInt32(result);
                    }
                }
            }
            // Debugging log di server
            Console.WriteLine($"Stock for {productID}: {stock}");
            return stock;


        }
    }

}