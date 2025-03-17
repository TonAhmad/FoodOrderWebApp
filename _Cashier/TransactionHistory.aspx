<%@ Page Title="Transaction History" Language="C#" MasterPageFile="~/CashierMaster.Master" AutoEventWireup="true" CodeBehind="TransactionHistory.aspx.cs" Inherits="Project2._Cashier.TransactionHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .history-container {
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
    <div class="history-container">
        <h1>Transaction History</h1>

        <div class="gridview-container">
            <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="TransactionID" OnRowCommand="gvTransactions_RowCommand">
                <Columns>
                    <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" />
                    <asp:BoundField DataField="TransactionDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="Rp {0:N0}" />

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnViewDetails" runat="server" CommandName="ViewDetails"
                                CommandArgument='<%# Eval("TransactionID") %>' Text="View Details"
                                CssClass="btn btn-info" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
