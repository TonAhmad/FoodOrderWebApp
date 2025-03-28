﻿<%@ Page Title="Process Payment" Language="C#" MasterPageFile="~/CashierMaster.Master" AutoEventWireup="true" CodeBehind="ProcessPayment.aspx.cs" Inherits="Project2._Cashier.ProcessPayment" %>

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
                <asp:Repeater ID="rptConfirmedOrders" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("orderID") %></td>
                            <td><%# Eval("customerName") %></td>
                            <td><%# Eval("orderDate", "{0:dd/MM/yyyy HH:mm}") %></td>
                            <td><%# Eval("orderStatus") %></td>
                            <td>
                                <asp:Button ID="btnSelect" runat="server" CssClass="btn-select"
                                    Text="Select" CommandArgument='<%# Eval("orderID") %>'
                                    OnClick="btnSelect_Click" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>


    <!-- Detail Pesanan yang Dipilih -->
    <div class="selected-order-container">
        <h3>Selected Order Details</h3>
        <table class="order-table">
            <thead>
                <tr>
                    <th>Customer Name</th>
                    <th>Product Name</th>
                    <th>Quantity</th>
                    <th>Subtotal</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptSelectedOrder" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("customerName") %>
                                <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("orderID") %>' Visible="false"></asp:Label>
                            </td>
                            <td>
                                <%# Eval("productName") %>
                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("productID") %>' Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("quantity") %>'></asp:Label>
                            </td>
                            <td>Rp
        <asp:Label ID="lblSubtotal" runat="server" Text='<%# Eval("subtotal", "{0:N0}") %>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>


    <!-- Input untuk Pembayaran -->
    <div class="payment-section">
        <h3 class="mb-3">Payment</h3>

        <!-- Total Amount -->
        <div class="mb-2">
            <label class="fw-bold">Total Amount:</label>
            <asp:Label ID="lblTotalAmount" runat="server" CssClass="ms-2 text-danger fw-bold"></asp:Label>
        </div>

        <!-- Amount Paid -->
        <div class="mb-2">
            <label for="txtAmount" class="form-label">Amount Paid:</label>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" placeholder="Masukkan nominal"></asp:TextBox>
        </div>

        <!-- Change -->
        <div class="mb-2">
            <label for="txtChange" class="form-label">Change:</label>
            <asp:TextBox ID="txtChange" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
        </div>

        <!-- Buttons -->
        <div class="d-flex gap-2 mt-3">
            <asp:Button ID="btnPay" runat="server" CssClass="btn btn-success px-4" Text="Pay" OnClick="btnPay_Click" />
            <asp:Button ID="btnCompleteTransaction" runat="server" CssClass="btn btn-primary px-4" Text="Complete Transaction" OnClick="btnCompleteTransaction_Click" />
        </div>
    </div>
</asp:Content>


