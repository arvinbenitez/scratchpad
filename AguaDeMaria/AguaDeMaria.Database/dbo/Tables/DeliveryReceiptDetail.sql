CREATE TABLE [dbo].[DeliveryReceiptDetail] (
    [DeliveryReceiptDetailId] INT             IDENTITY (1, 1) NOT NULL,
    [DeliveryReceiptId]       INT             NOT NULL,
    [ProductTypeId]           INT             NOT NULL,
    [Quantity]                INT             NOT NULL,
    [UnitPrice]               DECIMAL (10, 2) CONSTRAINT [DF_DeliveryReceiptDetail_PricePerPiece] DEFAULT ((0)) NOT NULL,
    [Amount]                  DECIMAL (10, 2) CONSTRAINT [DF_DeliveryReceiptDetail_Amount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DeliveryReceiptDetail] PRIMARY KEY CLUSTERED ([DeliveryReceiptDetailId] ASC),
    CONSTRAINT [FK_DeliveryReceiptDetail_DeliveryReceipt] FOREIGN KEY ([DeliveryReceiptId]) REFERENCES [dbo].[DeliveryReceipt] ([DeliveryReceiptId]),
    CONSTRAINT [FK_DeliveryReceiptDetail_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductType] ([ProductTypeId])
);

