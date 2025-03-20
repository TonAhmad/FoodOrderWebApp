<%@ Page Title="Process Payment" Language="C#" MasterPageFile="~/CashierMaster.Master" AutoEventWireup="true" CodeBehind="ProcessPayment.aspx.cs" Inherits="Project2._Cashier.ProcessPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center">Daftar Pesanan Pending</h2>

    <asp:Label ID="lblMessage" runat="server" CssClass="message-label"></asp:Label>

    <div class="table-responsive">
        <table class="order-table">
            <thead>
                <tr>
                    <th>Order ID</th>
                    <th>Customer Name</th>
                    <th>Order Date</th>
                    <th>Order Status</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPendingOrders" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("orderID") %></td>
                            <td><%# Eval("customerName") %></td>
                            <td><%# Eval("orderDate", "{0:dd/MM/yyyy HH:mm}") %></td>
                            <td><%# Eval("orderStatus") %></td>
                            <td>
                                <asp:Button ID="btnConfirm" runat="server" CssClass="btn-confirm"
                                    Text="Confirm" CommandArgument='<%# Eval("orderID") %>'
                                    OnClick="btnConfirm_Click" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <!-- Tabel Pesanan yang Sudah Dikonfirmasi -->
<h3>Confirmed Orders</h3>
<table class="order-table">
    <thead>
        <tr>
            <th>Order ID</th>
            <th>Customer Name</th>
            <th>Product Name</th>
            <th>Quantity</th>
            <th>Subtotal</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="rptConfirmedOrders" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("orderID") %></td>
                    <td><%# Eval("customerName") %></td>
                    <td><%# Eval("productName") %></td>
                    <td><%# Eval("quantity") %></td>
                    <td>Rp <%# Eval("subtotal", "{0:N0}") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

<!-- Input untuk Pembayaran -->
<div class="payment-section">
    <h3>Payment</h3>
    <div class="payment-row">
        <label for="txtAmount">Amount Paid:</label>
        <asp:TextBox ID="txtAmount" runat="server" CssClass="payment-input" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
    </div>
    <div class="payment-row">
        <label for="txtChange">Change:</label>
        <asp:TextBox ID="txtChange" runat="server" CssClass="payment-input" ReadOnly="true"></asp:TextBox>
    </div>
    <asp:Button ID="btnCompleteTransaction" runat="server" CssClass="btn-complete" Text="Complete Transaction" OnClick="btnCompleteTransaction_Click" />
</div>
</asp:Content>


