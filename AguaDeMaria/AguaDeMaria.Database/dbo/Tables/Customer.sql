CREATE TABLE [dbo].[Customer] (
    [CustomerId]     INT            IDENTITY (1, 1) NOT NULL,
    [CustomerCode]   NVARCHAR (50)  NULL,
    [CustomerName]   NVARCHAR (100) NOT NULL,
    [CustomerTypeId] INT            NOT NULL,
    [Address]        NVARCHAR (200) NOT NULL,
    [GPSCoordinate]  NVARCHAR (50)  NULL,
    [ContactNumbers] NVARCHAR (200) NULL,
    [Notes]          NVARCHAR (500) NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerId] ASC),
    CONSTRAINT [FK_Customer_CustomerType] FOREIGN KEY ([CustomerTypeId]) REFERENCES [dbo].[CustomerType] ([CustomerTypeId])
);

