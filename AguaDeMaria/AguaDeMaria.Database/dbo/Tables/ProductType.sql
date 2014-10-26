CREATE TABLE [dbo].[ProductType] (
    [ProductTypeId]  INT            IDENTITY (1, 1) NOT NULL,
    [ProductType]    NVARCHAR (100) NOT NULL,
    [Description]    NVARCHAR (100) NULL,
    [BasePrice]      DECIMAL (9, 2) NOT NULL,
    [IsInvetoriable] INT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED ([ProductTypeId] ASC)
);

