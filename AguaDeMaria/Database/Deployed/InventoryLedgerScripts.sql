IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='InventoryLedgerType')
BEGIN
	CREATE TABLE InventoryLedgerType
	(
		InventoryLedgerTypeId INT NOT NULL IDENTITY,
		Name NVARCHAR(100),
		CONSTRAINT PK_InventoryLedgerType PRIMARY KEY CLUSTERED (InventoryLedgerTypeId)
	)
	SET IDENTITY_INSERT InventoryLedgerType ON
	INSERT INTO InventoryLedgerType (InventoryLedgerTypeId,Name)
	VALUES
	(1,'Delivery'),
	(2,'Pickup Slip'),
	(3,'Adjustment')
	SET IDENTITY_INSERT InventoryLedgerType OFF
END

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='InventoryLedger')
BEGIN
	CREATE TABLE InventoryLedger
	(
		InventoryLedgerId INT NOT NULL IDENTITY,
		InventoryLedgerTypeId INT NOT NULL,
		PostDate DATETIME NOT NULL,
		CustomerId INT NOT NULL,
		ReferenceId INT NOT NULL,
		ProductTypeId INT NOT NULL,
		Quantity INT NOT NULL,
		Notes NVARCHAR(100),
		CONSTRAINT PK_InventoryLedger PRIMARY KEY CLUSTERED (InventoryLedgerId),
		CONSTRAINT FK_InventoryLedger_InventoryLedgerType FOREIGN KEY (InventoryLedgerTypeId) REFERENCES InventoryLedgerType(InventoryLedgerTypeId),
		CONSTRAINT FK_InventoryLedger_ProductType FOREIGN KEY (ProductTypeId) REFERENCES ProductType(ProductTypeId),
		CONSTRAINT FK_InventoryLedger_Customer FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
	)
END
GO

ALTER VIEW [dbo].[Inventory]
AS
SELECT 
	INV.TransactionId,
	INV.TransactionType,
	INV.RefNumber,
	INV.RefDate,
	C.CustomerId,
	C.CustomerName,
	ISNULL(INV.[Slim],0) as Slim,
	ISNULL(INV.[Round],0) as Round

FROM
Customer C
LEFT JOIN
(
	SELECT 
			PickupSlipId		as TransactionId,
		   'Pickup Slip'		as TransactionType,
		   PickupSlipNumber		as RefNumber,
		   PickupDate			as RefDate,
		   CustomerId			as CustomerId,
		   CustomerName			as CustomerName,
		   [Slim],
		   [Round]
	FROM
	(
	SELECT C.CustomerName, Ps.PickupSlipId, PS.PickupSlipNumber, PS.CustomerId, PS.PickupDate, P.ProductType, -1 * PDS.Quantity as Quantity
	FROM 
		PickupSlip PS 
		INNER JOIN PickupSlipDetail PDS on PS.PickupSlipId = PDS.PickupSlipId
		INNER JOIN ProductType P on PDS.ProductTypeId = P.ProductTypeId
		INNER JOIN Customer C on PS.CustomerId = C.CustomerId
		INNER JOIN InventoryLedger IL ON PS.PickupSlipId = IL.ReferenceId AND IL.ProductTypeId = P.ProductTypeId AND IL.InventoryLedgerTypeId = 2
	) Source
	PIVOT
	(
	SUM(Quantity)
	FOR ProductType IN ([Slim],[Round])
	) as PivotTable
	UNION ALL
	SELECT 
			DeliveryReceiptId	as TransactionId,
		   'Delivery'			as TransactionType,
		   DRNumber				as RefNumber,
		   DRDate				as RefDate,
		   CustomerId			as CustomerId,
		   CustomerName			as CustomerName,
		   [Slim],
		   [Round]
	FROM
	(
	SELECT C.CustomerName, DRD.DeliveryReceiptId, DR.DRNumber, DR.CustomerId, DR.DRDate, P.ProductType, DRD.Quantity as Quantity
	FROM 
		DeliveryReceipt DR
		INNER JOIN DeliveryReceiptDetail DRD on DR.DeliveryReceiptId = DRD.DeliveryReceiptId
		INNER JOIN ProductType P on DRD.ProductTypeId = P.ProductTypeId
		INNER JOIN Customer C on DR.CustomerId = C.CustomerId
		INNER JOIN InventoryLedger IL ON DR.DeliveryReceiptId = IL.ReferenceId AND IL.ProductTypeId = P.ProductTypeId AND IL.InventoryLedgerTypeId = 1
	) Source
	PIVOT
	(
	SUM(Quantity)
	FOR ProductType IN ([Slim],[Round])
	) as PivotTable
	UNION ALL
	SELECT 
			ReferenceId	as TransactionId,
		   'Adjustment'			as TransactionType,
		   RefNumber			as RefNumber,
		   PostDate				as RefDate,
		   CustomerId			as CustomerId,
		   CustomerName			as CustomerName,
		   [Slim],
		   [Round]
	FROM
	(
	SELECT C.CustomerName, 100 AS ReferenceId, '' as RefNumber, IL.CustomerId, IL.PostDate, P.ProductType, IL.Quantity
	FROM 
		InventoryLedger IL
		INNER JOIN Customer C on IL.CustomerId = C.CustomerId
		INNER JOIN ProductType P on IL.ProductTypeId = P.ProductTypeId
	WHERE IL.InventoryLedgerTypeId = 3
	) Source
	PIVOT
	(
	SUM(Quantity)
	FOR ProductType IN ([Slim],[Round])
	) as PivotTable
) Inv on C.CustomerId = INV.CustomerId
