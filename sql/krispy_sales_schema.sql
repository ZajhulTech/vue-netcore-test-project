

create database demo_control;
use demo_control;

-- =========================
-- ELIMINACIÓN DE TABLAS
-- =========================
DROP VIEW IF EXISTS vwSaleDetails;

DROP TABLE IF EXISTS sale_details;
DROP TABLE IF EXISTS sales;
DROP TABLE IF EXISTS products;
DROP TABLE IF EXISTS users;

-- =========================
-- CREACIÓN DE TABLAS
-- =========================

CREATE TABLE users (
    id              INT IDENTITY(1,1) NOT NULL,
    userName        NVARCHAR(100) NOT NULL,
    passwordHash    NVARCHAR(255) NOT NULL,
    fullName        NVARCHAR(150),
    createdAt       DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    createdBy       NVARCHAR(100) NOT NULL,
    updatedAt       DATETIME2 NULL,
    updatedBy       NVARCHAR(100),
    CONSTRAINT PK_Users_Id PRIMARY KEY (id),
    CONSTRAINT UQ_Users_UserName UNIQUE (userName)
);

CREATE TABLE products (
    id          INT IDENTITY(1,1) NOT NULL,
    name        NVARCHAR(150) NOT NULL,
    price       DECIMAL(10,2) NOT NULL,
    createdAt   DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    createdBy   NVARCHAR(100) NOT NULL,
    updatedAt   DATETIME2 NULL,
    updatedBy   NVARCHAR(100),
    CONSTRAINT PK_Products_Id PRIMARY KEY (id)
);

CREATE TABLE sales (
    id          INT IDENTITY(1,1) NOT NULL,
    description NVARCHAR(200) NOT NULL,
    amount      DECIMAL(10,2) NOT NULL,
    date        DATETIME2 NOT NULL,
    createdAt   DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    createdBy   NVARCHAR(100) NOT NULL,
    updatedAt   DATETIME2 NULL,
    updatedBy   NVARCHAR(100),
    CONSTRAINT PK_Sales_Id PRIMARY KEY (id)
);

CREATE TABLE sale_details (
    id         INT IDENTITY(1,1) NOT NULL,
    saleId     INT NOT NULL,
    productId  INT NOT NULL,
    quantity   INT NOT NULL,
    unitPrice  DECIMAL(10,2) NOT NULL,
    CONSTRAINT PK_SaleDetails_Id PRIMARY KEY (id),
    CONSTRAINT FK_SaleDetail_Sale FOREIGN KEY (saleId) REFERENCES sales(id),
    CONSTRAINT FK_SaleDetail_Product FOREIGN KEY (productId) REFERENCES products(id)
);

INSERT INTO users (userName, passwordHash, fullName, createdBy)
VALUES (
    'admin',
    '$2a$12$SzxflAPnIICcQ4JLkRkTQeKmPy6pA/hMI18awE/cnkZVKHpupLHv2',  -- password = 1234
    'Administrator',
    'system'
);

INSERT INTO products (name, price, createdBy) VALUES
('Chocolate Donut', 22.50, 'admin'),
('Glazed Donut', 18.00, 'admin'),
('Strawberry Filled Donut', 25.00, 'admin');

