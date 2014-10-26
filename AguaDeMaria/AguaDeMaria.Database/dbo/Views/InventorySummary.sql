CREATE VIEW [dbo].[InventorySummary]
AS
SELECT 
	CustomerId, 
	CustomerName, 
	MAX(CASE WHEN TransactionType='Pickup Slip' THEN RefDate ELSE NULL END) LatestPickupDate,
	MAX(CASE WHEN TransactionType='Delivery' THEN RefDate ELSE NULL END) LatestDeliveryDate,
	SUM(Slim) as Slim,
	SUM(Round) as Round
FROM Inventory
GROUP BY CustomerId, CustomerName
