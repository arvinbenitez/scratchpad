CREATE TABLE [dbo].[OrderStatus] (
    [OrderStatusId] INT           IDENTITY (1, 1) NOT NULL,
    [StatusName]    NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED ([OrderStatusId] ASC)
);

