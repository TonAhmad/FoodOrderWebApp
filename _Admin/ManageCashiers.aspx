<%@ Page Title="Manage Cashiers" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageCashiers.aspx.cs" Inherits="Project2._Admin.ManageCashiers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card {
            box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="text-center mb-4">Manage Cashiers</h2>

        <!-- Form Tambah/Edit Cashier -->
        <div class="card p-4">
            <h5 class="card-title text-center">Add / Edit Cashier</h5>
            <div class="row">
                <div class="col-md-6">
                    <label class="form-label">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="form-label">Full Name</label>
                    <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-md-6">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="form-label">Phone Number</label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-md-6">
                    <label class="form-label">Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="form-label">Role</label>
                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Admin" Value="admin"></asp:ListItem>
                        <asp:ListItem Text="Cashier" Value="cashier"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-md-6">
                    <label class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="text-center mt-4">
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success me-2" OnClick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-warning me-2" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
            </div>
        </div>

        <!-- GridView -->
        <div class="card p-4 mt-4">
            <h5 class="card-title text-center">Cashier List</h5>
            <div class="table-responsive">
                <asp:GridView ID="gvAdmins" runat="server" AutoGenerateColumns="False" DataKeyNames="username"
                    CssClass="table table-striped table-bordered text-center" GridLines="None"
                    OnSelectedIndexChanged="gvAdmins_SelectedIndexChanged" AutoPostBack="True">
                    <Columns>
                        <asp:BoundField DataField="admin_id" HeaderText="Admin ID" ReadOnly="True" />
                        <asp:BoundField DataField="username" HeaderText="Username" ReadOnly="True" />
                        <asp:BoundField DataField="fullname" HeaderText="Full Name" ReadOnly="True" />
                        <asp:BoundField DataField="email" HeaderText="Email" ReadOnly="True" />
                        <asp:BoundField DataField="phone_number" HeaderText="Phone" ReadOnly="True" />
                        <asp:BoundField DataField="address" HeaderText="Address" ReadOnly="True" />
                        <asp:BoundField DataField="role" HeaderText="Role" ReadOnly="True" />
                        <asp:ButtonField Text="Select" CommandName="Select" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
