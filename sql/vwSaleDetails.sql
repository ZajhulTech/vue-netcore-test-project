

-- ========================================
--  CREACIÓN DE VISTA
-- ========================================
CREATE VIEW vwSaleDetails AS
SELECT
    s.id AS SaleId,
    s.description,
    s.amount,
    s.date,
    s.createdAt,
    s.createdBy,
    sd.id AS SaleDetailId,
    sd.productId,
    p.name AS ProductName,
    sd.quantity,
    sd.unitPrice,
    (sd.quantity * sd.unitPrice) AS Subtotal
FROM sales s
INNER JOIN sale_details sd ON s.id = sd.saleId
INNER JOIN products p ON sd.productId = p.id;