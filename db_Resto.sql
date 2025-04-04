CREATE DATABASE db_Resto;

-- Schema
CREATE SCHEMA adm;
CREATE SCHEMA item;
CREATE SCHEMA orders;
CREATE SCHEMA transactions;

-- Tabel Admin (Cashier & Admin)
CREATE TABLE adm.Admin (
    admin_id VARCHAR(10) PRIMARY KEY,
    fullname VARCHAR(100) NOT NULL,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    phone_number VARCHAR(15) NOT NULL,
    address TEXT NOT NULL,
    created_at DATETIME2 DEFAULT GETDATE(),
    role VARCHAR(10) NOT NULL CHECK (role IN ('admin', 'cashier'))
);


-- Tabel Kategori Produk
CREATE TABLE item.Category (
    categoryID VARCHAR(10) PRIMARY KEY,
    categoryName VARCHAR(100) NOT NULL UNIQUE,
    createdAt DATETIME DEFAULT GETDATE()
);

-- Tabel Produk
CREATE TABLE item.Product (
    productID VARCHAR(10) PRIMARY KEY,
    productName VARCHAR(100) NOT NULL UNIQUE,
    categoryID VARCHAR(10) NOT NULL,
    price DECIMAL(10,2) NOT NULL CHECK (price >= 0),
    stock INT NOT NULL CHECK (stock >= 0),
    createdAt DATETIME DEFAULT GETDATE(),
	imagePath NVARCHAR(255) NOT NULL,
    FOREIGN KEY (categoryID) REFERENCES item.Category(categoryID) ON DELETE CASCADE
);

-- Tabel OrderHeader (Pesanan Customer)
CREATE TABLE orders.OrderHeader (
    orderID VARCHAR(100) PRIMARY KEY,
    customerName VARCHAR(100) NOT NULL, -- Customer wajib isi nama
    admin_id VARCHAR(10) NULL, -- Akan diisi setelah cashier menangani pesanan
    orderStatus VARCHAR(10) NOT NULL CHECK (orderStatus IN ('pending', 'confirmed', 'completed', 'canceled')),
    orderDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (admin_id) REFERENCES adm.Admin(admin_id) ON DELETE SET NULL -- Jika cashier dihapus, order tetap ada
);


-- Tabel OrderDetail (Detail Pesanan)
CREATE TABLE orders.OrderDetail (
    orderID VARCHAR(100),
    productID VARCHAR(10) NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    subtotal DECIMAL(10,2),
    PRIMARY KEY (orderID, productID),
    FOREIGN KEY (orderID) REFERENCES orders.OrderHeader(orderID) ON DELETE CASCADE,
    FOREIGN KEY (productID) REFERENCES item.Product(productID) ON DELETE CASCADE
);

-- Tabel TransHeader (Transaksi yang Sudah Dibayar)
CREATE TABLE Transactions.TransHeader (
    transID VARCHAR(50) PRIMARY KEY,
    orderID VARCHAR(100) NOT NULL, -- Hubungkan transaksi ke order
    admin_id VARCHAR(10) NOT NULL, -- Admin yang merekap transaksi
    total MONEY NOT NULL,
    transDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (orderID) REFERENCES orders.OrderHeader(orderID) ON DELETE CASCADE,
    FOREIGN KEY (admin_id) REFERENCES adm.Admin(admin_id) ON DELETE NO ACTION
);


-- Tabel TransDetail (Detail Transaksi yang Sudah Dibayar)
CREATE TABLE Transactions.TransDetail (
    transID VARCHAR(50) NOT NULL,
    productID VARCHAR(10) NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    subtotal MONEY,
    PRIMARY KEY (transID, productID),
    FOREIGN KEY (transID) REFERENCES Transactions.TransHeader(transID) ON DELETE CASCADE,
    FOREIGN KEY (productID) REFERENCES item.product(productID) ON DELETE CASCADE
);

-- TRIGGER Untuk auto generate ID admin
CREATE TRIGGER before_insert_admin
ON adm.Admin
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @next_id INT;
    DECLARE @next_admin_id VARCHAR(10);

    -- Ambil angka terbesar dari ID yang ada
    SELECT @next_id = COALESCE(MAX(CAST(SUBSTRING(admin_id, 4, LEN(admin_id)) AS INT)), 0) + 1
    FROM adm.Admin;

    -- Format ID menjadi ADM001, ADM002, ...
    SET @next_admin_id = 'ADM' + RIGHT('000' + CAST(@next_id AS VARCHAR(3)), 3);

    -- Masukkan data ke dalam tabel termasuk fullname
    INSERT INTO adm.Admin (admin_id, fullname, username, email, phone_number, address, password_hash, role, created_at)
    SELECT @next_admin_id, fullname, username, email, phone_number, address, password_hash, role, GETDATE()
    FROM inserted;
END;


-- TRIGGER Untuk auto generate ID category
CREATE TRIGGER before_insert_category
ON item.category
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @nextID INT;
    DECLARE @newCategoryID VARCHAR(10);

    -- Ambil angka terbesar dari categoryID yang ada, lalu tambahkan 1
    SELECT @nextID = COALESCE(MAX(CAST(SUBSTRING(categoryID, 4, LEN(categoryID)) AS INT)), 0) + 1
    FROM item.category;

    -- Format categoryID menjadi CAT001, CAT002, ...
    SET @newCategoryID = 'CAT' + RIGHT('000' + CAST(@nextID AS VARCHAR(3)), 3);

    -- Masukkan data ke dalam tabel
    INSERT INTO item.category (categoryID, categoryName, createdAt)
    SELECT @newCategoryID, categoryName, GETDATE()
    FROM inserted;
END;

-- TRIGGER Untuk auto generate ID product
CREATE TRIGGER before_insert_product
ON item.product
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @nextID INT;
    DECLARE @newProductID VARCHAR(10);

    -- Ambil angka terbesar dari productID yang ada, lalu tambahkan 1
    SELECT @nextID = COALESCE(MAX(CAST(SUBSTRING(productID, 4, LEN(productID)) AS INT)), 0) + 1
    FROM item.product;

    -- Format productID menjadi PRD001, PRD002, ...
    SET @newProductID = 'PRD' + RIGHT('000' + CAST(@nextID AS VARCHAR(3)), 3);

    -- Masukkan data ke dalam tabel
    INSERT INTO item.product (productID, productName, categoryID, price, stock, imagePath, createdAt)
    SELECT @newProductID, productName, categoryID, price, stock, imagePath, GETDATE()
    FROM inserted;
END;

--Otomatis mengurangi stok setelah checkout.
CREATE TRIGGER trg_UpdateStockAfterOrder
ON orders.OrderDetail
AFTER INSERT
AS
BEGIN
    UPDATE item.Product
    SET stock = stock - od.quantity
    FROM item.Product p
    INNER JOIN inserted od ON p.productID = od.productID;
END;

