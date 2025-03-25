using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2.Models;

namespace Project2._Admin
{
    public partial class ManageProducts : System.Web.UI.Page
    {
        Category category = new Category();
        Product product = new Product();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                LoadDDCat();
                LoadProducts();
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                ShowAlert("danger", "Category name cannot be empty!");
                return;
            }

            category.categoryName = txtCategoryName.Text;
            string result = category.Create();

            if (result == "success")
            {
                ShowAlert("success", "Category added successfully!");
                ClearCat();
                LoadCategories();
                LoadDDCat();
            }
            else
            {
                ShowAlert("danger", result);
            }
        }

        protected void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryID.Text))
            {
                ShowAlert("danger", "Please select a category first!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                ShowAlert("danger", "Category name cannot be empty!");
                return;
            }

            category.categoryID = txtCategoryID.Text;
            category.categoryName = txtCategoryName.Text;
            string result = category.Update();

            if (result == "success")
            {
                ShowAlert("success", "Category updated successfully!");
                ClearCat();
                LoadCategories();
                LoadDDCat();
            }
            else
            {
                ShowAlert("danger", result);
            }
        }

        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryID.Text))
            {
                ShowAlert("danger", "Please select a category first!");
                return;
            }

            category.categoryID = txtCategoryID.Text;
            string result = category.Delete();

            if (result == "success")
            {
                ShowAlert("success", "Category deleted successfully!");
                ClearCat();
                LoadCategories();
                LoadDDCat();
            }
            else
            {
                ShowAlert("danger", result);
            }
        }

        protected void gvCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvCategory.SelectedRow;
            txtCategoryID.Text = row.Cells[0].Text;
            txtCategoryName.Text = row.Cells[1].Text;
            ShowAlert("primary", "Category selected!");
        }

        void LoadCategories()
        {
            gvCategory.DataSource = category.Read();
            gvCategory.DataBind();
        }

        private void ClearCat()
        {
            txtCategoryID.Text = "";
            txtCategoryName.Text = "";
        }
        private void ClearFields()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            ddlCategory.SelectedIndex = 0;
            txtPrice.Text = "";
            txtStock.Text = "";
            imgPreview.Visible = false;

        }

        private void ShowAlert(string type, string message)
        {
            alertMessage.Attributes["class"] = "alert alert-" + type;
            alertMessage.InnerText = message;
            alertMessage.Visible = true;
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Validasi input
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || ddlCategory.SelectedValue == "0" ||
                string.IsNullOrWhiteSpace(txtPrice.Text) || string.IsNullOrWhiteSpace(txtStock.Text))
            {
                ShowAlert("danger", "All fields must be filled!");
                return;
            }

            // Cek apakah file diunggah
            if (!fileUpload.HasFile)
            {
                ShowAlert("danger", "Please upload a product image!");
                return;
            }

            try
            {
                // Tentukan folder penyimpanan
                string folderPath = Server.MapPath("~/ProductImages/");

                // Pastikan folder ada
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Buat nama file unik
                string fileName = Path.GetFileName(fileUpload.FileName);
                string filePath = folderPath + fileName;
                string dbPath = "/ProductImages/" + fileName;

                // Simpan file ke folder
                fileUpload.SaveAs(filePath);

                // Isi data produk
                product.productName = txtProductName.Text;
                product.categoryID = ddlCategory.SelectedValue;
                product.price = Convert.ToDecimal(txtPrice.Text);
                product.stock = Convert.ToInt32(txtStock.Text);
                product.imagePath = dbPath;

                // Simpan ke database
                string result = product.Create();
                if (result == "success")
                {
                    ShowAlert("success", "Product added successfully!");
                    ClearFields();
                    LoadProducts();
                    LoadDDCat();

                    // Tampilkan preview gambar
                    imgPreview.ImageUrl = dbPath;
                    imgPreview.Visible = true;
                }
                else
                {
                    ShowAlert("danger", result);
                }
            }
            catch (Exception ex)
            {
                ShowAlert("danger", "Error: " + ex.Message);
            }
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                ShowAlert("danger", "Please select a product first!");
                return;
            }

            product.productID = txtProductID.Text;
            product.productName = txtProductName.Text;
            product.categoryID = ddlCategory.SelectedValue;
            product.price = Convert.ToDecimal(txtPrice.Text);
            product.stock = Convert.ToInt32(txtStock.Text);

            // Jika admin tidak upload gambar baru, gunakan gambar lama dari ViewState
            if (fileUpload.HasFile)
            {
                string filename = Path.GetFileName(fileUpload.FileName);
                string filepath = "/ProductImages/" + filename;
                fileUpload.SaveAs(Server.MapPath(filepath));
                product.imagePath = filepath;
            }
            else
            {
                product.imagePath = ViewState["CurrentImage"]?.ToString();
            }

            string result = product.Update();
            if (result == "success")
            {
                ShowAlert("success", "Product updated successfully!");
                ClearFields();
                LoadProducts();
                LoadDDCat();
            }
            else
            {
                ShowAlert("danger", result);
            }
        }

        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                ShowAlert("danger", "Please select a category first!");
                return;
            }

            product.productID = txtProductID.Text;
            string result = product.Delete();

            if (result == "success")
            {
                ShowAlert("success", "Product deleted successfully!");
                LoadProducts();
                ClearCat();
                LoadCategories();
                LoadDDCat();
            }
            else
            {
                ShowAlert("danger", result);
            }
        }

        private void LoadProducts()
        {
            gvProduct.DataSource = product.Read(); // Ambil data dari model `product`
            gvProduct.DataBind();

            // Update Label Halaman
            lblPageNumberProduct.Text = $"Page {gvProduct.PageIndex + 1} of {gvProduct.PageCount}";

            // Nonaktifkan tombol jika sudah di awal/akhir
            btnPrevPageProduct.Enabled = gvProduct.PageIndex > 0;
            btnNextPageProduct.Enabled = gvProduct.PageIndex < gvProduct.PageCount - 1;
        }

        // Event untuk GridView paging
        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProduct.PageIndex = e.NewPageIndex;
            LoadProducts();
        }

        // Event untuk tombol Previous Page
        protected void btnPrevPageProduct_Click(object sender, EventArgs e)
        {
            if (gvProduct.PageIndex > 0)
            {
                gvProduct.PageIndex--;
                LoadProducts();
            }
        }

        // Event untuk tombol Next Page
        protected void btnNextPageProduct_Click(object sender, EventArgs e)
        {
            if (gvProduct.PageIndex < gvProduct.PageCount - 1)
            {
                gvProduct.PageIndex++;
                LoadProducts();
            }
        }

        private void LoadDDCat()
        {
            try
            {
                Category category = new Category();
                DataTable dt = category.GetCategories();

                // Bind data ke DropDownList
                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "categoryName";
                ddlCategory.DataValueField = "categoryID";
                ddlCategory.DataBind();

                // Tambahkan opsi default
                ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", ""));
            }
            catch (Exception ex)
            {
                ShowAlert("danger", "Failed to load categories: " + ex.Message);
            }
        }


        protected void gvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvProduct.SelectedRow;

            txtProductID.Text = row.Cells[0].Text;
            txtProductName.Text = row.Cells[1].Text;
            ddlCategory.SelectedValue = row.Cells[2].Text;
            txtPrice.Text = row.Cells[3].Text;
            txtStock.Text = row.Cells[4].Text;

            // Ambil path gambar dari GridView
            string imagePath = ((Image)row.FindControl("imgProduct")).ImageUrl;

            // Set image preview jika ada gambar
            if (!string.IsNullOrEmpty(imagePath))
            {
                imgPreview.ImageUrl = imagePath;
                imgPreview.Visible = true;
                ViewState["CurrentImage"] = imagePath; // Simpan gambar lama di ViewState
            }
        }
    }
}