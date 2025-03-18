<%@ Page Title="Cashier Dashboard" Language="C#" MasterPageFile="~/CashierMaster.Master" AutoEventWireup="true" CodeBehind="DashboardC.aspx.cs" Inherits="Project2._Cashier.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cashier-container">
        <h1>Cashier Dashboard</h1>

        <div class="gridview-container">
            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="Rp {0:N0}" />
                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="Rp {0:N0}" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="total-section">
            <h3>Total: <asp:Label ID="lblTotal" runat="server" Text="Rp 0"></asp:Label></h3>
            <asp:Button ID="btnCheckout" runat="server" Text="Checkout" CssClass="btn btn-primary" OnClick="btnCheckout_Click" />
        </div>
    </div>
</asp:Content>
