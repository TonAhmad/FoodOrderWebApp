<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Project2._Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
        <h1 class="mb-4">Admin Dashboard</h1>
        <div class="row">
            <div class="col-md-3">
                <div class="card text-white bg-primary dashboard-card">
                    <div class="card-body">
                        <h5 class="card-title">Total Kategori</h5>
                        <p class="card-text">
                            <asp:Label ID="lblTotalCategories" runat="server" CssClass="h3"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-success dashboard-card">
                    <div class="card-body">
                        <h5 class="card-title">Total Produk</h5>
                        <p class="card-text">
                            <asp:Label ID="lblTotalProducts" runat="server" CssClass="h3"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-warning dashboard-card">
                    <div class="card-body">
                        <h5 class="card-title">Total Transaksi</h5>
                        <p class="card-text">
                            <asp:Label ID="lblTotalTransactions" runat="server" CssClass="h3"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-danger dashboard-card">
                    <div class="card-body">
                        <h5 class="card-title">Total Pendapatan</h5>
                        <p class="card-text">
                            <asp:Label ID="lblTotalRevenue" runat="server" CssClass="h3"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <h3 class="mt-4">Produk dengan Stok Rendah</h3>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th>Nama Produk</th>
                    <th>Kategori</th>
                    <th>Stok</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptLowStock" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("productName") %></td>
                            <td><%# Eval("categoryName") %></td>
                            <td><%# Eval("stock") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
