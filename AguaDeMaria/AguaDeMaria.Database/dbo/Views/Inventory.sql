CREATE VIEW Inventory
AS
SELECT * 
FROM
(
	SELECT 
			PickupSlipId			as TransactionId,
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
	) Source
	PIVOT
	(
	SUM(Quantity)
	FOR ProductType IN ([Slim],[Round])
	) as PivotTable
) Inv

