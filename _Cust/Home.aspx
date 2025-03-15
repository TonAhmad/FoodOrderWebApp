<%@ Page Title="" Language="C#" MasterPageFile="~/ServeMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Project2._Cust.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="carouselExampleIndicators" class="carousel slide" data-bs-ride="carousel">
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active"></button>
            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1"></button>
            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2"></button>
        </div>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="Images/burger.jpg" class="d-block w-100" alt="Burger Lezat">
            </div>
            <div class="carousel-item">
                <img src="Images/pizza.jpg" class="d-block w-100" alt="Pizza Spesial">
            </div>
            <div class="carousel-item">
                <img src="Images/dessert.jpg" class="d-block w-100" alt="Dessert Manis">
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
            <span class="carousel-control-prev-icon"></span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
            <span class="carousel-control-next-icon"></span>
        </button>
    </div>

    <section class="container mt-5">
        <h2 class="text-center mb-4">Menu Favorit Kami</h2>
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <img src="Images/burger.jpg" class="card-img-top" alt="Burger">
                    <div class="card-body text-center">
                        <h5 class="card-title">Cheese Burger</h5>
                        <p class="card-text">Burger lezat dengan keju melimpah.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <img src="Images/pizza.jpg" class="card-img-top" alt="Pizza">
                    <div class="card-body text-center">
                        <h5 class="card-title">Pepperoni Pizza</h5>
                        <p class="card-text">Pizza dengan topping pepperoni dan keju.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <img src="Images/dessert.jpg" class="card-img-top" alt="Dessert">
                    <div class="card-body text-center">
                        <h5 class="card-title">Choco Lava Cake</h5>
                        <p class="card-text">Dessert coklat meleleh di dalam.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="text-center mt-5">
        <h2>Pesan Sekarang</h2>
        <p>Rasakan makanan lezat kami, cukup pesan dari web ini!</p>
        <a href="order.aspx" class="btn btn-warning btn-lg">Order Sekarang</a>
    </section>

</asp:Content>
