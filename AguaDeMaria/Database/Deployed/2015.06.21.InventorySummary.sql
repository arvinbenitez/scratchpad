ALTER VIEW [dbo].[InventorySummary]
AS
SELECT 
	Inv.CustomerId,
	Inv.CustomerName,
	ISNULL(Inv.LatestDeliveryDate, DR.RefDate) as LatestDeliveryDate,
	Inv.LatestPickupDate,
	Inv.Round,
	Inv.Slim
FROM
(SELECT 
	CustomerId, 
	CustomerName, 
	MAX(CASE WHEN TransactionType='Pickup Slip' THEN RefDate ELSE NULL END) LatestPickupDate,
	MAX(CASE WHEN TransactionType='Delivery' THEN RefDate ELSE NULL END) LatestDeliveryDate,
	SUM(Slim) as Slim,
	SUM(Round) as Round
FROM Inventory
GROUP BY CustomerId, CustomerName) Inv
LEFT JOIN (SELECT CustomerId, MAX(DrDate) as RefDate 
	       FROM DeliveryReceipt DR 
		   GROUP BY CustomerId) as DR on Inv.CustomerId = Dr.CustomerId
