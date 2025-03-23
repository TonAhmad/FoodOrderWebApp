using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2.Models;

namespace Project2._Cust
{
    public partial class Menu : System.Web.UI.Page
    {
        ProductMenu productMenu = new ProductMenu();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                LoadProducts();
            }
        }

        private void LoadCategories()
        {
            ProductMenu productMenu = new ProductMenu();
            rptCategories.DataSource = productMenu.GetCategories();
            rptCategories.DataBind();
        }

        private void LoadProducts()
        {
            ProductMenu productMenu = new ProductMenu();
            rptProducts.DataSource = productMenu.GetProductsWithCategoryName();
            rptProducts.DataBind();
        }
    }
}