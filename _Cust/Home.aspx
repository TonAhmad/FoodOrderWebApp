<%@ Page Title="" Language="C#" MasterPageFile="~/ServeMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Project2._Cust.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="carouselExampleIndicators" class="carousel slide" data-bs-ride="carousel">
    <div class="carousel-indicators">
        <asp:Repeater ID="rptCarouselIndicators" runat="server">
            <ItemTemplate>
                <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="<%# Container.ItemIndex %>" 
                        class='<%# Container.ItemIndex == 0 ? "active" : "" %>'></button>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="carousel-inner">
        <asp:Repeater ID="rptCarousel" runat="server">
            <ItemTemplate>
                <div class='carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>'>
                    <img src='<%# Eval("ImagePath") %>' class="d-block w-100" alt='<%# Eval("ProductName") %>'>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
        <span class="carousel-control-prev-icon"></span>
    </button>
    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
        <span class="carousel-control-next-icon"></span>
    </button>
</div>

    <section class="container mt-5">
        <h2 class="text-center mb-4">Top Seller</h2>
        <div class="row">
            <asp:Repeater ID="RepeaterTopSeller" runat="server">
                <ItemTemplate>
                    <div class="col-md-4">
                        <div class="card">
                            <img src='<%# Eval("ImagePath") %>' class="card-img-top" alt='<%# Eval("ProductName") %>'>
                            <div class="card-body text-center">
                                <h5 class="card-title"><%# Eval("ProductName") %></h5>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

    <section class="text-center mt-5">
        <h2>Pesan Sekarang</h2>
        <p>Rasakan makanan lezat kami, cukup pesan dari web ini!</p>
        <a href="Menu.aspx" class="btn btn-warning btn-lg">Order Sekarang</a>
    </section>
</asp:Content>

