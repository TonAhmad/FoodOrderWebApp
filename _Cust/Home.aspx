﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ServeMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Project2._Cust.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Section Welcome -->
    <section class="container mt-5 text-center">
        <h2>Selamat Datang di Restoran Kami</h2>
        <p class="lead">Nikmati kelezatan makanan terbaik kami, dibuat dengan bahan berkualitas tinggi dan cita rasa yang tak terlupakan.</p>
    </section>

    <!-- Top Seller -->
    <section class="container mt-5">
        <h2 class="text-center mb-4">🔥 Menu Favorit Pilihan Pelanggan 🔥</h2>
        <p class="text-center text-muted">Cek beberapa menu terlaris yang paling banyak dipesan oleh pelanggan setia kami!</p>
        <div class="row justify-content-center">
            <asp:Repeater ID="RepeaterTopSeller" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 d-flex justify-content-center">
                        <div class="card text-center shadow-sm">
                            <img src='<%# Eval("ImagePath") %>' class="card-img-top mx-auto mt-3" alt='<%# Eval("ProductName") %>'>
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("ProductName") %></h5>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

        <!-- Image Slider (Auto-Scrolling) -->
    <div class="scrolling-container mt-3">
        <div class="scrolling-wrapper">
            <asp:Repeater ID="rptScrollingImages" runat="server">
                <ItemTemplate>
                    <div class="scrolling-item">
                        <img src='<%# Eval("ImagePath") %>' alt='<%# Eval("ProductName") %>' class="scrolling-image">
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <!-- Order Section -->
    <section class="text-center mt-5">
        <h2>🍽️ Pesan Sekarang, Nikmati Kelezatannya! 🍽️</h2>
        <p class="mb-4">Tak perlu antri! Pesan makanan favoritmu langsung dari web ini dan nikmati setiap gigitan lezatnya.</p>
        <a href="Menu.aspx" class="btn btn-warning btn-lg mt-3">Order Sekarang</a>
    </section>

</asp:Content>



