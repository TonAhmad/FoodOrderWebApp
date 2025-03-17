<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="Project2._Admin.ManageProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <h1 class="text-center">Manage Products</h1>

    <!-- Alert -->
    <div id="alertMessage" runat="server" class="alert" role="alert" visible="false"></div>

    <!-- Form Tambah/Update Kategori -->
    <div class="card p-4 mt-4">
        <h5 class="card-title text-center">Add / Update Category</h5>
        <div class="row">
            <div class="col-md-6">
                <label class="form-label">Category ID</label>
                <asp:TextBox ID="txtCategoryID" runat="server" CssClass="form-control" ReadOnly="True" Enabled="False"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="form-label">Category Name</label>
                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="text-center mt-3">
            <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" CssClass="btn btn-success me-2"
                OnClick="btnAddCategory_Click" />
            <asp:Button ID="btnUpdateCategory" runat="server" Text="Update Category" CssClass="btn btn-warning me-2"
                OnClick="btnUpdateCategory_Click" />
            <asp:Button ID="btnDeleteCategory" runat="server" Text="Delete Category" CssClass="btn btn-danger"
                OnClick="btnDeleteCategory_Click" />
        </div>
    </div>

    <!-- Tabel Daftar Kategori -->
    <div class="card p-4 mt-4">
        <h5 class="card-title text-center">Category List</h5>
        <div class="table-responsive">
            <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered text-center" GridLines="None"
                OnSelectedIndexChanged="gvCategory_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="categoryID" HeaderText="Category ID" ReadOnly="True" />
                    <asp:BoundField DataField="categoryName" HeaderText="Category Name" ReadOnly="True" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Select" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <h1 class="text-center">Manage Products</h1>

    <!-- Alert -->
    <div id="Div1" runat="server" class="alert" role="alert" visible="false"></div>

    <!-- Form Tambah/Update Produk -->
    <div class="card p-4 mt-4">
        <h5 class="card-title text-center">Add / Update Product</h5>
        <div class="row">
            <div class="col-md-6">
                <label class="form-label">Product ID</label>
                <asp:TextBox ID="txtProductID" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="form-label">Product Name</label>
                <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="form-label">Category</label>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <label class="form-label">Price</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="form-label">Stock</label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="form-label">Upload Image</label>
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
            </div>
        </div>

        <div class="text-center mt-3">
            <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" CssClass="btn btn-success me-2"
                OnClick="btnAddProduct_Click" />
            <asp:Button ID="btnUpdateProduct" runat="server" Text="Update Product" CssClass="btn btn-warning me-2"
                OnClick="btnUpdateProduct_Click" />
            <asp:Button ID="btnDeleteProduct" runat="server" Text="Delete Product" CssClass="btn btn-danger"
                OnClick="btnDeleteProduct_Click" />
        </div>
    </div>

    <!-- Tabel Produk -->
    <div class="card p-4 mt-4">
        <h5 class="card-title text-center">Product List</h5>
        <div class="table-responsive">
            <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered text-center" GridLines="None"
                OnSelectedIndexChanged="gvProduct_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="productID" HeaderText="Product ID" ReadOnly="True" />
                    <asp:BoundField DataField="productName" HeaderText="Product Name" ReadOnly="True" />
                    <asp:BoundField DataField="categoryID" HeaderText="Category" ReadOnly="True" />
                    <asp:BoundField DataField="price" HeaderText="Price" ReadOnly="True" />
                    <asp:BoundField DataField="stock" HeaderText="Stock" ReadOnly="True" />
                    <asp:BoundField DataField="imagePath" HeaderText="Image" ReadOnly="True" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Select" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
