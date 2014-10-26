CREATE TABLE [dbo].[OrderDetail] (
    [OrderDetailId] INT IDENTITY (1, 1) NOT NULL,
    [OrderId]       INT NOT NULL,
    [ProductTypeId] INT NOT NULL,
    [Qty]           INT NOT NULL,
    CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED ([OrderDetailId] ASC),
    CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId]),
    CONSTRAINT [FK_OrderDetail_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductType] ([ProductTypeId])
);

