using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2._Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardData();
                LoadLowStockProducts();
            }
        }

        private void LoadDashboardData()
        {
            string connString = ConfigurationManager.ConnectionStrings["Set"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                // Total kategori
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM item.Category", con))
                {
                    lblTotalCategories.Text = cmd.ExecuteScalar().ToString();
                }

                // Total produk
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM item.Product", con))
                {
                    lblTotalProducts.Text = cmd.ExecuteScalar().ToString();
                }

                // Total transaksi
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Transactions.TransHeader", con))
                {
                    lblTotalTransactions.Text = cmd.ExecuteScalar().ToString();
                }

                // Total pendapatan
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(total), 0) FROM Transactions.TransHeader", con))
                {
                    lblTotalRevenue.Text = "Rp " + Convert.ToDecimal(cmd.ExecuteScalar()).ToString("N2");
                }
            }
        }

        private void LoadLowStockProducts()
        {
            string connString = ConfigurationManager.ConnectionStrings["Set"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string query = @"
                    SELECT p.productName, c.categoryName, p.stock 
                    FROM item.Product p 
                    JOIN item.Category c ON p.categoryID = c.categoryID 
                    WHERE p.stock <= 5 
                    ORDER BY p.stock ASC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptLowStock.DataSource = dt;
                        rptLowStock.DataBind();
                    }
                }
            }
        }
    }
}