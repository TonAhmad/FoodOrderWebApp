<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Project2._Admin.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1 class="mb-4">Laporan Keuangan</h1>

        <!-- Total Earnings -->
        <div class="card text-white bg-success mb-4">
            <div class="card-body">
                <h4 class="card-title">Total Pendapatan</h4>
                <p class="card-text">
                    <asp:Label ID="lblTotalEarnings" runat="server" CssClass="h2 font-weight-bold"></asp:Label>
                </p>
            </div>
        </div>

      <!-- Laporan Transaksi -->
<h2>Laporan Transaksi</h2>

<!-- Form Pencarian -->
<div class="search-container mb-3">
    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Transaction ID or Order ID"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mt-2" OnClick="btnSearch_Click" />
    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary mt-2" OnClick="btnReset_Click" />
</div>

<!-- GridView Transaksi -->
<asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="false"
    CssClass="table table-striped" PageSize="5" AllowPaging="true"
    OnPageIndexChanging="gvTransactions_PageIndexChanging">
    <Columns>
        <asp:BoundField DataField="transID" HeaderText="Transaction ID" />
        <asp:BoundField DataField="orderID" HeaderText="Order ID" />
        <asp:TemplateField HeaderText="Total">
            <ItemTemplate>
                Rp. <%# Eval("total") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="transDate" HeaderText="Transaction Date" DataFormatString="{0:dd-MM-yyyy}" />
    </Columns>
</asp:GridView>

<!-- Tombol Navigasi Paging -->
<div class="paging-buttons mt-3">
    <asp:Button ID="btnPrevPage" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnPrevPage_Click" />
    <asp:Label ID="lblPageNumber" runat="server" CssClass="page-info mx-3" />
    <asp:Button ID="btnNextPage" runat="server" Text="Next" CssClass="btn btn-primary" OnClick="btnNextPage_Click" />
</div>


        <!-- Laporan Penjualan Produk -->
        <h2>Laporan Penjualan Produk</h2>
        <div class="row mb-3">
            <!-- Dropdown Filter -->
            <div class="col-md-3">
                <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="true"
                    CssClass="form-select" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                    <asp:ListItem Value="day">Harian</asp:ListItem>
                    <asp:ListItem Value="month">Bulanan</asp:ListItem>
                </asp:DropDownList>
            </div>

            <!-- Input Box -->
            <div class="col-md-4">
                <asp:TextBox ID="txtFilterValue" runat="server" CssClass="form-control"
                    placeholder="Masukkan Hari/Bulan"></asp:TextBox>
            </div>

            <!-- Tombol Filter -->
            <div class="col-md-2">
                <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn btn-primary"
                    OnClick="ddlFilter_SelectedIndexChanged" />
            </div>
        </div>

        <div class="scrollable-table">
            <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="false"
                CssClass="table table-striped" PageSize="5">
                <Columns>
                    <asp:BoundField DataField="productName" HeaderText="Product Name" />
                    <asp:BoundField DataField="TotalSold" HeaderText="Quantity Sold" />
                    <asp:TemplateField HeaderText="Total Earnings">
                        <ItemTemplate>
                            Rp. <%# Eval("TotalEarnings", "{0:N0}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>


