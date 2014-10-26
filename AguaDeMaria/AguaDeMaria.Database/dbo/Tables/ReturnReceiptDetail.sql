CREATE TABLE [dbo].[ReturnReceiptDetail] (
    [ReturnReceiptId]       INT            NOT NULL,
    [ReturnReceiptDetailId] INT            IDENTITY (1, 1) NOT NULL,
    [ProductTypeId]         INT            NOT NULL,
    [Quantity]              DECIMAL (9, 2) NOT NULL,
    CONSTRAINT [PK_ReturnReceiptDetail] PRIMARY KEY CLUSTERED ([ReturnReceiptDetailId] ASC),
    CONSTRAINT [FK_ReturnReceiptDetail_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductType] ([ProductTypeId]),
    CONSTRAINT [FK_ReturnReceiptDetail_ReturnReceipt] FOREIGN KEY ([ReturnReceiptId]) REFERENCES [dbo].[ReturnReceipt] ([ReturnReceiptId])
);

