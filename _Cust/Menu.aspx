﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ServeMaster.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Project2._Cust.Menu" %>

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
                                <div id="cat<%# Eval("categoryID") %>" class="col-12 mb-3">
                                    <div class="product-card <%# Convert.ToInt32(Eval("stock")) == 0 ? "out-of-stock" : "" %>">
                                        <!-- Gambar Produk -->
                                        <div class="product-image">
                                            <img src='<%# Eval("imagePath") %>' alt="Product Image">
                                        </div>

                                        <!-- Deskripsi Produk -->
                                        <div class="product-details">
                                            <h5 class="card-title"><%# Eval("productName") %></h5>
                                            <p class="card-text"><strong>Category:</strong> <%# Eval("categoryName") %></p>
                                            <p class="card-text">Harga: <strong>Rp <%# Eval("price", "{0:N0}") %></strong></p>
                                            <p class="card-text"><strong>Stok:</strong> <%# Eval("stock") %></p>

                                            <button class="btn btn-primary add-to-cart"
                                                onclick="addToCart('<%# Eval("productID") %>', '<%# Eval("productName") %>', <%# Eval("price") %>)"
                                                <%# Convert.ToInt32(Eval("stock")) == 0 ? "disabled" : "" %>>
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
                <form id="checkout" runat="server">
                    <div class="cart-container">
                        <h4>Keranjang Anda</h4>
                        <div id="cartItems"></div>
                        <p><strong>Total: Rp <span id="totalPrice">0</span></strong></p>
                        <!-- Tombol Checkout -->
                        <div class="text-center mt-3">
                            <asp:Button ID="btnCheckout" runat="server" Text="Checkout" CssClass="btn btn-success" OnClientClick="saveCartAndRedirect(); return false;" />
                        </div>

                    </div>
                </form>
            </div>
        </div>
    </div>

    <script>
        let cart = {};

        function addToCart(productID, productName, price) {
            console.log("Checking stock for product:", productID); // Debug log

            $.ajax({
                type: "POST",
                url: "../ProductService.aspx/GetProductStock",
                data: JSON.stringify({ productID: productID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log("Stock received:", response.d); // Debug log
                    let stock = response.d; // Pastikan ini adalah angka
                    let currentQuantity = cart[productID] ? cart[productID].quantity : 0;

                    if (stock > 0 && currentQuantity < stock) {
                        // Jika produk sudah ada di cart, tambahkan jumlahnya
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
                        console.log("Product added to cart:", cart);
                    } else {
                        alert("Stok tidak mencukupi!");

                        // **Tambahan: Ubah tampilan jika stok sudah habis**
                        updateProductStatus(productID, 0);
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error fetching stock:", xhr.responseText);
                }
            });
        }


        function updateProductStatus(productID, stock) {
            if (stock === 0) {
                let productCard = document.querySelector(`#cat${productID} .product-card`);
                let button = document.querySelector(`#cat${productID} .add-to-cart`);

                if (productCard) productCard.classList.add("out-of-stock");
                if (button) {
                    button.disabled = true;
                    button.style.backgroundColor = "#ccc";
                    button.style.cursor = "not-allowed";
                }
            }
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

        function saveCartAndRedirect() {
            let cartArray = Object.keys(cart).map(id => ({
                productID: id,
                name: cart[id].name,
                price: cart[id].price,
                quantity: cart[id].quantity
            }));

            localStorage.setItem("cart", JSON.stringify(cartArray));
            window.location.href = "Checkout.aspx";

            fetch("Checkout.aspx?saveCart=1", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(cartArray),
            }).then(response => {
                if (response.ok) {
                    localStorage.removeItem("cart");
                    window.location.href = "Checkout.aspx";
                }
            }).catch(error => console.error("Error:", error));
        }

    </script>

</asp:Content>
