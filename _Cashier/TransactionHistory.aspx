﻿<%@ Page Title="Transaction History" Language="C#" MasterPageFile="~/CashierMaster.Master" AutoEventWireup="true" CodeBehind="TransactionHistory.aspx.cs" Inherits="Project2._Cashier.TransactionHistory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="history-container">
        <h1>Order History</h1>

<!-- Pencarian Transaksi -->
<div class="search-container mb-3">
    <asp:TextBox ID="txtSearchTransaction" runat="server" CssClass="form-control" placeholder="Search by Order ID"></asp:TextBox>
    <asp:Button ID="btnSearchTransaction" runat="server" Text="Search" CssClass="btn btn-primary mt-2" OnClick="btnSearchTransaction_Click" />
    <asp:Button ID="btnResetTransaction" runat="server" Text="Reset" CssClass="btn btn-secondary mt-2" OnClick="btnResetTransaction_Click" />
</div>

<!-- Tabel Transaksi -->
<div class="gridview-container">
    <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
        DataKeyNames="orderID" OnRowCommand="gvTransactions_RowCommand"
        AllowPaging="True" PageSize="5" OnPageIndexChanging="gvTransactions_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="orderID" HeaderText="Order ID" />
            <asp:BoundField DataField="orderDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnViewDetails" runat="server" CommandName="ViewDetails"
                        CommandArgument='<%# Eval("orderID") %>' Text="View Details"
                        CssClass="btn btn-info" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<!-- Navigasi Halaman -->
<div class="text-center mt-3">
    <asp:Button ID="btnPrevPageTransaction" runat="server" Text="Previous" CssClass="btn btn-secondary"
        OnClick="btnPrevPageTransaction_Click" />
    <asp:Label ID="lblPageNumberTransaction" runat="server" CssClass="mx-3"></asp:Label>
    <asp:Button ID="btnNextPageTransaction" runat="server" Text="Next" CssClass="btn btn-secondary"
        OnClick="btnNextPageTransaction_Click" />
</div>

    </div>
</asp:Content>
