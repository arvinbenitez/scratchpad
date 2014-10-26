CREATE TABLE [dbo].[SalesInvoice] (
    [SalesInvoiceId] INT            IDENTITY (1, 1) NOT NULL,
    [InvoiceNumber]  NVARCHAR (50)  NOT NULL,
    [InvoiceDate]    DATETIME       NOT NULL,
    [CustomerId]     INT            NOT NULL,
    [Remarks]        NVARCHAR (200) NULL,
    [OrderId]        INT            NULL,
    CONSTRAINT [PK_SalesInvoice] PRIMARY KEY CLUSTERED ([SalesInvoiceId] ASC),
    CONSTRAINT [FK_SalesInvoice_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [FK_SalesInvoice_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId])
);

