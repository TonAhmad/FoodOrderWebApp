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

        private void LoadTransactionReport()
        {
            DataTable dt = reportModel.GetTransactionReports();
            gvTransactions.DataSource = dt;
            gvTransactions.DataBind();
        }

        private void LoadSalesReport()
        {
            DataTable dt = reportModel.GetSalesReport("month", DateTime.Now.Month);
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