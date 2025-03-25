using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;
using Project2.Models;

namespace Project2._Admin
{
    public partial class Reports : System.Web.UI.Page
    {
        Report reportModel = new Report();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTransactionReport();
            LoadSalesReport();
            LoadTotalEarnings();
        }

        private void LoadTransactionReport(string searchQuery = "")
        {
            DataTable dt = reportModel.GetTransactionReports(); // Ambil semua data transaksi

            // Jika ada query pencarian, filter data berdasarkan transID atau orderID
            if (!string.IsNullOrEmpty(searchQuery))
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = $"transID LIKE '%{searchQuery}%' OR orderID LIKE '%{searchQuery}%'";
                dt = dv.ToTable();
            }

            // Format kolom "total"
            foreach (DataRow row in dt.Rows)
            {
                row["total"] = Convert.ToDecimal(row["total"]).ToString("N0"); // Format tanpa angka desimal
            }

            gvTransactions.DataSource = dt;
            gvTransactions.DataBind();

            // Update halaman label paging
            lblPageNumber.Text = $"Page {gvTransactions.PageIndex + 1} of {gvTransactions.PageCount}";

            // Nonaktifkan tombol jika di awal/akhir halaman
            btnPrevPage.Enabled = gvTransactions.PageIndex > 0;
            btnNextPage.Enabled = gvTransactions.PageIndex < gvTransactions.PageCount - 1;
        }

        // Event Paging GridView
        protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransactions.PageIndex = e.NewPageIndex;
            LoadTransactionReport(txtSearch.Text); // Load ulang dengan search query jika ada
        }

        // Event Search Button
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadTransactionReport(txtSearch.Text);
        }

        // Event Reset Button
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadTransactionReport(); // Kembali ke data awal tanpa filter
        }

        // Event untuk tombol Previous Page
        protected void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (gvTransactions.PageIndex > 0)
            {
                gvTransactions.PageIndex--;
                LoadTransactionReport(txtSearch.Text);
            }
        }

        // Event untuk tombol Next Page
        protected void btnNextPage_Click(object sender, EventArgs e)
        {
            if (gvTransactions.PageIndex < gvTransactions.PageCount - 1)
            {
                gvTransactions.PageIndex++;
                LoadTransactionReport(txtSearch.Text);
            }
        }


        private void LoadSalesReport()
        {
            DataTable dt = reportModel.GetSalesReport("month", DateTime.Now.Month);

            // Format kolom "subtotal" sebelum di-bind ke GridView
            foreach (DataRow row in dt.Rows)
            {
                row["TotalEarnings"] = Convert.ToDecimal(row["TotalEarnings"]).ToString("N0"); // Format tanpa angka di belakang koma
            }

            gvSales.DataSource = dt;
            gvSales.DataBind();
        }


        private void LoadTotalEarnings()
        {
            decimal totalEarnings = reportModel.GetTotalEarnings();
            lblTotalEarnings.Text = "Total Pendapatan: Rp " + totalEarnings.ToString("N0");
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterType = ddlFilter.SelectedValue;
            string filterValue = txtFilterValue.Text.Trim();

            // Jika filterValue kosong, tampilkan semua data tanpa filter
            if (string.IsNullOrEmpty(filterValue))
            {
                gvSales.DataSource = reportModel.GetSalesReport("", 0);
            }
            else
            {
                // Cek apakah input hanya berisi angka
                if (int.TryParse(filterValue, out int parsedValue))
                {
                    gvSales.DataSource = reportModel.GetSalesReport(filterType, parsedValue);

                }
                else
                {
                    lblTotalEarnings.Text = "Format input salah! Masukkan angka yang valid.";
                    return;
                }
            }

            gvSales.DataBind();
        }
    }
}