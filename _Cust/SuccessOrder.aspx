<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuccessOrder.aspx.cs" Inherits="Project2._Cust.SuccessOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Konfirmasi pesanan</title>
    <style>
        .order-summary {
            max-width: 400px; /* Ukuran card agar proporsional */
            margin: 20px auto;
            padding: 15px;
            border-radius: 8px;
            background: #f8f9fa;
        }

        .order-info {
            text-align: left; /* Order ID dan Nama rata kiri */
            font-size: 14px;
            margin: 5px 0;
        }

        .order-item {
            display: flex;
            justify-content: space-between;
            padding: 5px 0;
        }

            .order-item.header {
                font-weight: bold;
                border-bottom: 2px solid #ddd;
                padding-bottom: 8px;
            }

        .item-name {
            flex: 2;
            text-align: left; /* Nama barang rata kiri */
        }

        .item-qty, .item-price {
            flex: 1;
            text-align: right;
        }

        .order-total {
            margin-top: 10px;
            font-size: 16px;
            font-weight: bold;
            text-align: right;
            border-top: 2px solid #ddd;
        }
    </style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container mt-5 text-center">
        <h2>Pesanan Berhasil!</h2>
        <p>
            Terima kasih <strong>
                <asp:Literal ID="litCustomerName" runat="server"></asp:Literal></strong> telah melakukan pemesanan.
        </p>

        <div class="container mt-5">
            <h3 class="text-center">Rincian Pesanan</h3>
            <div class="order-summary">
                <p class="order-info"><strong>Order ID:</strong>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal></p>
                <p class="order-info"><strong>Nama:</strong>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal></p>

                <div class="order-item header">
                    <span class="item-name">Nama Barang</span>
                    <span class="item-qty">Jumlah</span>
                    <span class="item-price">Harga</span>
                </div>

                <asp:Literal ID="litOrderDetails" runat="server"></asp:Literal>

                <div class="order-total">
                    Total:
                    <asp:Literal ID="litTotalPrice" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <p class="text-danger"><strong>Tunjukkan halaman ini ke kasir jika belum terkonfirmasi!</strong></p>
        <a href="Menu.aspx" class="btn btn-primary">Kembali ke Menu</a>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.min.js" integrity="sha384-0pUGZvbkm6XF6gxjEnlmuGrJXVbNuzT9qBBavbLwCsOGabYfZo0T0to5eqruptLy" crossorigin="anonymous"></script>
</body>
</html>
