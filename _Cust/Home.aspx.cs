using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2.Models;

namespace Project2._Cust
{
    public partial class Home : System.Web.UI.Page
    {
        readonly SqlConnection con = new SqlConnection(Koneksi.connString); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTopSeller();
                LoadCarousel();
            }
        }

        private void LoadTopSeller()
        {
            string query = @"
                SELECT TOP 3 p.productID, p.productName, p.imagePath, SUM(td.quantity) AS totalSold
                FROM Transactions.TransDetail td
                JOIN item.Product p ON td.productID = p.productID
                GROUP BY p.productID, p.productName, p.imagePath
                ORDER BY totalSold DESC";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                RepeaterTopSeller.DataSource = dt;
                RepeaterTopSeller.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadCarousel()
        {
            string query = "SELECT productName, imagePath FROM item.Product";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                rptCarousel.DataSource = dt;
                rptCarousel.DataBind();

                rptCarouselIndicators.DataSource = dt;
                rptCarouselIndicators.DataBind();
            }
        }
    }
}