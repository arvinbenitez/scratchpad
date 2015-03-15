CREATE View DailySummary
AS
SELECT 
	ISNULL(O.Date, ISNULL(D.Date,ISNULL(S.Date,ISNULL(P.DATE,null)))) as [Date] , O.RoundOrderQty, O.SlimOrderQty,
	D.RoundDeliveredQty, D.SlimDeliveredQty, D.RoundDeliveredAmt, D.SlimDeliveredAmt,
	S.Amount as CollectionAmount,
	P.RoundPickupQty, P.SlimPickupQty
FROM 
(SELECT 
	CONVERT(NVARCHAR(10),O.OrderDate, 110) AS [Date], 
	SUM(CASE WHEN OD.ProductTypeId = 1 THEN OD.Qty ELSE 0 END) as SlimOrderQty,
	SUM(CASE WHEN OD.ProductTypeId = 2 THEN OD.Qty ELSE 0 END) as RoundOrderQty
FROM [Order] O 
	INNER JOIN OrderDetail OD ON O.OrderId = OD.OrderId
GROUP BY CONVERT(NVARCHAR(10),O.OrderDate, 110)) O
FULL OUTER JOIN
(SELECT 
	CONVERT(NVARCHAR(10),DR.DRDate, 110) AS [Date], 
	SUM(CASE WHEN DRD.ProductTypeId = 1 THEN DRD.Quantity ELSE 0 END) as SlimDeliveredQty,
	SUM(CASE WHEN DRD.ProductTypeId = 2 THEN DRD.Quantity ELSE 0 END) as RoundDeliveredQty,
	SUM(CASE WHEN DRD.ProductTypeId = 1 THEN DRD.Amount ELSE 0 END) as SlimDeliveredAmt,
	SUM(CASE WHEN DRD.ProductTypeId = 2 THEN DRD.Amount ELSE 0 END) as RoundDeliveredAmt
FROM DeliveryReceipt DR
	INNER JOIN DeliveryReceiptDetail  DRD on DR.DeliveryReceiptId = DRD.DeliveryReceiptId
GROUP BY CONVERT(NVARCHAR(10),DR.DRDate, 110)) D ON O.Date = D.Date
FULL OUTER JOIN
(SELECT CONVERT(NVARCHAR(10),SI.InvoiceDate, 110) as [Date],
	SUM(SID.Amount) as Amount
FROM
	SalesInvoice SI
	INNER JOIN SalesInvoiceDetail SID on SI.SalesInvoiceId = SID.SalesInvoiceId
GROUP BY CONVERT(NVARCHAR(10),SI.InvoiceDate, 110)) S ON O.Date = S.Date
FULL OUTER JOIN
(SELECT	CONVERT(nvarchar(10),ps.PickupDate,110) as [Date],
	SUM(CASE WHEN PSD.ProductTypeId = 1 THEN PSD.Quantity ELSE 0 END) as SlimPickupQty,
	SUM(CASE WHEN PSD.ProductTypeId = 2 THEN PSD.Quantity ELSE 0 END) as RoundPickupQty
FROM PickupSlip PS
	INNER JOIN PickupSlipDetail PSD ON PS.PickupSlipId = PSD.PickupSlipId
GROUP BY CONVERT(nvarchar(10),ps.PickupDate,110)) P ON O.Date = P.Date
