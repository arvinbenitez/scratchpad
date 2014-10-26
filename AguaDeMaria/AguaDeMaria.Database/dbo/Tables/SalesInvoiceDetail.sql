CREATE TABLE [dbo].[SalesInvoiceDetail] (
    [SalesInvoiceDetailId] INT            IDENTITY (1, 1) NOT NULL,
    [SalesInvoiceId]       INT            NOT NULL,
    [ProductTypeId]        INT            NOT NULL,
    [Quantity]             DECIMAL (9, 2) NOT NULL,
    [Price]                DECIMAL (9, 2) NOT NULL,
    CONSTRAINT [PK_SalesInvoiceDetail] PRIMARY KEY CLUSTERED ([SalesInvoiceDetailId] ASC),
    CONSTRAINT [FK_SalesInvoiceDetail_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductType] ([ProductTypeId]),
    CONSTRAINT [FK_SalesInvoiceDetail_SalesInvoice] FOREIGN KEY ([SalesInvoiceId]) REFERENCES [dbo].[SalesInvoice] ([SalesInvoiceId])
);

