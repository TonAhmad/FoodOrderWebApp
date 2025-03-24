<%@ Page Title="Transaction History" Language="C#" MasterPageFile="~/CashierMaster.Master" AutoEventWireup="true" CodeBehind="TransactionHistory.aspx.cs" Inherits="Project2._Cashier.TransactionHistory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="history-container">
        <h1>Transaction History</h1>

        <div class="gridview-container">
        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
            DataKeyNames="orderID" OnRowCommand="gvTransactions_RowCommand">
            <Columns>
                <asp:BoundField DataField="orderID" HeaderText="Transaction ID" />
                <asp:BoundField DataField="orderDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="Rp {0:N0}" />

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
    </div>
</asp:Content>
