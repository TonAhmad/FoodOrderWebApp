<%@ Page Title="Process Payment" Language="C#" MasterPageFile="~/CashierMaster.Master" AutoEventWireup="true" CodeBehind="ProcessPayment.aspx.cs" Inherits="Project2._Cashier.ProcessPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .payment-container {
            width: 50%;
            margin: auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="payment-container">
        <h1>Process Payment</h1>

        <div class="form-group">
            <label>Total Amount:</label>
            <asp:Label ID="lblTotalAmount" runat="server" CssClass="form-control" Text="Rp 0"></asp:Label>
        </div>

        <div class="form-group">
            <label>Amount Paid:</label>
            <asp:TextBox ID="txtAmountPaid" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAmountPaid_TextChanged"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Change:</label>
            <asp:Label ID="lblChange" runat="server" CssClass="form-control" Text="Rp 0"></asp:Label>
        </div>

        <asp:Button ID="btnConfirmPayment" runat="server" Text="Confirm Payment" CssClass="btn btn-success" OnClick="btnConfirmPayment_Click" />
    </div>
</asp:Content>
