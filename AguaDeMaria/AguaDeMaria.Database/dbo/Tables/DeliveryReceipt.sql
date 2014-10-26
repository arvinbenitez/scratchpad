CREATE TABLE [dbo].[DeliveryReceipt] (
    [DeliveryReceiptId] INT           IDENTITY (1, 1) NOT NULL,
    [DRNumber]          NVARCHAR (50) NOT NULL,
    [DRDate]            DATETIME      NOT NULL,
    [CustomerId]        INT           NOT NULL,
    [OrderId]           INT           NULL,
    CONSTRAINT [PK_DeliveryReceipt] PRIMARY KEY CLUSTERED ([DeliveryReceiptId] ASC),
    CONSTRAINT [FK_DeliveryReceipt_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [FK_DeliveryReceipt_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId])
);

