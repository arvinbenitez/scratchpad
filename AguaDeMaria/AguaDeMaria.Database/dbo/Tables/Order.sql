CREATE TABLE [dbo].[Order] (
    [OrderId]       INT           IDENTITY (1, 1) NOT NULL,
    [OrderNumber]   NVARCHAR (50) NOT NULL,
    [CustomerId]    INT           NOT NULL,
    [OrderDate]     DATETIME      CONSTRAINT [DF_Order_OrderDate] DEFAULT (getdate()) NOT NULL,
    [OrderStatusId] INT           NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_Order_OrderStatus] FOREIGN KEY ([OrderStatusId]) REFERENCES [dbo].[OrderStatus] ([OrderStatusId])
);

