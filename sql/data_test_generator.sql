-- ===============================
-- Script de datos de prueba
-- 50 ventas con detalles
-- ===============================

-- Declaración para ID de ventas
DECLARE @saleId INT;
DECLARE @i INT = 1;

WHILE @i <= 50
BEGIN
    -- Insertar venta
    INSERT INTO sales (description, amount, date, createdAt, createdBy)
    VALUES (
        CASE ABS(CHECKSUM(NEWID())) % 4
            WHEN 0 THEN 'Online Order'
            WHEN 1 THEN 'In-Store Purchase'
            WHEN 2 THEN 'Special Event Sale'
            ELSE 'Birthday Promo'
        END,
        0,  -- Se actualizará luego
        DATEADD(DAY, ABS(CHECKSUM(NEWID())) % 30, '2025-06-01'),
        SYSUTCDATETIME(),
        'admin'
    );

    SET @saleId = SCOPE_IDENTITY();

    -- Generar de 1 a 4 productos por venta
    DECLARE @itemCount INT = 1 + ABS(CHECKSUM(NEWID())) % 4;
    DECLARE @j INT = 1;
    DECLARE @totalAmount DECIMAL(10,2) = 0;

    WHILE @j <= @itemCount
    BEGIN
        DECLARE @productId INT = 1 + ABS(CHECKSUM(NEWID())) % 3;
        DECLARE @quantity INT = 1 + ABS(CHECKSUM(NEWID())) % 5;
        DECLARE @unitPrice DECIMAL(10,2);

        -- Obtener precio según producto
        SET @unitPrice = 
            CASE @productId
                WHEN 1 THEN 22.50
                WHEN 2 THEN 18.00
                ELSE 25.00
            END;

        -- Insertar detalle de venta
        INSERT INTO sale_details (saleId, productId, quantity, unitPrice)
        VALUES (@saleId, @productId, @quantity, @unitPrice);

        SET @totalAmount += @quantity * @unitPrice;
        SET @j += 1;
    END

    -- Actualizar total de la venta
    UPDATE sales SET amount = @totalAmount WHERE id = @saleId;

    SET @i += 1;
END
