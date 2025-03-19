<%@ Page Title="" Language="C#" MasterPageFile="~/ServeMaster.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Project2._Cust.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="text-center">Menu</h2>
        <div class="row">
            <asp:Repeater ID="rptCategories" runat="server">
                <ItemTemplate>
                    <div class="col-auto">
                        <a href="#cat<%# Eval("categoryID") %>" class="btn btn-outline-primary mx-1 category-btn">
                            <%# Eval("categoryName") %>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <div class="menu-container">
                <!-- Bagian Kiri: Daftar Produk -->
                <div class="product-container">
                    <div class="row">
                        <asp:Repeater ID="rptProducts" runat="server">
                            <ItemTemplate>
                                <%-- Tambahkan ID unik untuk kategori --%>
                                <div id="cat<%# Eval("categoryID") %>" class="col-12 mb-3">
                                    <div class="product-card">
                                        <!-- Gambar Produk -->
                                        <div class="product-image">
                                            <img src='<%# Eval("imagePath") %>' alt="Product Image">
                                        </div>

                                        <!-- Deskripsi Produk -->
                                        <div class="product-details">
                                            <h5 class="card-title"><%# Eval("productName") %></h5>
                                            <p class="card-text"><strong>Category:</strong> <%# Eval("categoryName") %></p>
                                            <p class="card-text">Harga: <strong>Rp <%# Eval("price", "{0:N0}") %></strong></p>
                                            <button class="btn btn-primary" onclick="addToCart('<%# Eval("productID") %>', '<%# Eval("productName") %>', <%# Eval("price") %>)">
                                                Tambah Produk
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <!-- Bagian Kanan: Cart -->
                <div class="cart-container">
                    <h4>Keranjang Anda</h4>
                    <div id="cartItems"></div>
                    <p><strong>Total: Rp <span id="totalPrice">0</span></strong></p>
                </div>
            </div>
        </div>
    </div>

    <script>
        let cart = {};

        function addToCart(productID, productName, price) {
            if (cart[productID]) {
                cart[productID].quantity += 1;
            } else {
                cart[productID] = {
                    name: productName,
                    price: price,
                    quantity: 1
                };
            }
            updateCart();
        }

        function updateCart() {
            let cartContainer = document.getElementById("cartItems");
            let totalPriceElement = document.getElementById("totalPrice");
            cartContainer.innerHTML = "";
            let total = 0;

            for (let id in cart) {
                let item = cart[id];
                let itemTotal = item.price * item.quantity;
                total += itemTotal;

                cartContainer.innerHTML += `
                <div class="cart-item">
                    <span>${item.name} - Rp ${itemTotal.toLocaleString()}</span>
                    <button onclick="decreaseQuantity('${id}')">-</button>
                    <span>${item.quantity}</span>
                    <button onclick="increaseQuantity('${id}')">+</button>
                </div>
            `;
            }

            totalPriceElement.innerText = total.toLocaleString();
        }

        function increaseQuantity(productID) {
            cart[productID].quantity += 1;
            updateCart();
        }

        function decreaseQuantity(productID) {
            if (cart[productID].quantity > 1) {
                cart[productID].quantity -= 1;
            } else {
                delete cart[productID];
            }
            updateCart();
        }
    </script>

</asp:Content>
