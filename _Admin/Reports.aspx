<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Project2._Admin.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Laporan Keuangan</h1>

    <asp:Label ID="lblTotalEarnings" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>

    <h2>Laporan Transaksi</h2>
    <div class="table-responsive" style="max-height: 250px; overflow-y: auto;">
        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="true"
            CssClass="table table-striped">
        </asp:GridView>
    </div>

    <h2>Laporan Penjualan Produk</h2>
    <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
        <asp:ListItem Value="day">Harian</asp:ListItem>
        <asp:ListItem Value="month">Bulanan</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="txtFilterValue" runat="server" placeholder="Masukkan Hari/Bulan"></asp:TextBox>
    <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="ddlFilter_SelectedIndexChanged" />

    <div class="table-responsive" style="max-height: 250px; overflow-y: auto;">
        <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="true"
            CssClass="table table-striped">
        </asp:GridView>
    </div>
</asp:Content>

