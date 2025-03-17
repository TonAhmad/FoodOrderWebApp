<%@ Page Title="Transaction Details" Language="C#" MasterPageFile="~/CashierMaster.Master" AutoEventWireup="true" CodeBehind="TransactionDetails.aspx.cs" Inherits="Project2._Cashier.TransactionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .details-container {
            width: 80%;
            margin: auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
        }
        .gridview-container {
            margin-top: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="details-container">
        <h1>Transaction Details</h1>
        <h3>Transaction ID: <asp:Label ID="lblTransactionID" runat="server"></asp:Label></h3>
        <h3>Date: <asp:Label ID="lblTransactionDate" runat="server"></asp:Label></h3>
        <h3>Total Amount: <asp:Label ID="lblTotalAmount" runat="server"></asp:Label></h3>

        <div class="gridview-container">
            <asp:GridView ID="gvTransactionDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="Rp {0:N0}" />
                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="Rp {0:N0}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
