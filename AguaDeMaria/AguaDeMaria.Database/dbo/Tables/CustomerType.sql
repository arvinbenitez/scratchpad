CREATE TABLE [dbo].[CustomerType] (
    [CustomerTypeId]   INT            IDENTITY (1, 1) NOT NULL,
    [CustomerTypeName] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_CustomerType] PRIMARY KEY CLUSTERED ([CustomerTypeId] ASC)
);

