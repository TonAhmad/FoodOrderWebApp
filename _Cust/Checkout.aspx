<%@ Page Title="" Language="C#" MasterPageFile="~/ServeMaster.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Project2._Cust.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formCO" runat="server">
        <div class="checkout-container">
            <h3>Checkout Pesanan</h3>

            <!-- Form Nama Pelanggan -->
            <div class="mb-3">
                <label for="txtCustomerName" class="form-label">Nama Pelanggan</label>
                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" placeholder="Masukkan nama Anda"></asp:TextBox>
            </div>

            <!-- Tabel Cart -->
            <h4>Pesanan Anda</h4>
            <table class="table">
                <thead>
                    <tr>
                        <th>Produk</th>
                        <th>Harga</th>
                        <th>Jumlah</th>
                        <th>Subtotal</th>
                    </tr>
                </thead>
                <tbody id="cartTable">
                    <!-- Cart akan diisi dengan JavaScript -->
                </tbody>
            </table>

            <p><strong>Total Harga: Rp <span id="checkoutTotal">0</span></strong></p>

            <!-- Tombol Konfirmasi Checkout -->
           <asp:Button ID="btnConfirmCheckout" runat="server" Text="Konfirmasi Checkout"
    CssClass="btn btn-primary w-100" OnClick="btnConfirmCheckout_Click"
    EnableViewState="true" />

        </div>
    </form>

    <script>
        // Ambil cart dari localStorage
        let cart = JSON.parse(localStorage.getItem("cart")) || [];
        console.log(cart); // Debugging untuk melihat isi cart

        function loadCart() {
            let cartTable = document.getElementById("cartTable");
            let totalElement = document.getElementById("checkoutTotal");
            cartTable.innerHTML = "";
            let total = 0;

            for (let id in cart) {
                let item = cart[id];
                let itemTotal = item.price * item.quantity;
                total += itemTotal;

                cartTable.innerHTML += `
                <tr>
                    <td>${item.name}</td>
                    <td>Rp ${item.price.toLocaleString()}</td>
                    <td>${item.quantity}</td>
                    <td>Rp ${itemTotal.toLocaleString()}</td>
                </tr>
            `;
            }

            totalElement.innerText = total.toLocaleString();
        }

        document.addEventListener("DOMContentLoaded", loadCart);
    </script>
</asp:Content>


