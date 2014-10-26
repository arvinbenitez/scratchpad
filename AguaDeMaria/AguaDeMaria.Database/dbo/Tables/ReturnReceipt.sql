CREATE TABLE [dbo].[ReturnReceipt] (
    [ReturnReceiptId]   INT      IDENTITY (1, 1) NOT NULL,
    [ReturnReceiptDate] DATETIME NOT NULL,
    [CustomerId]        INT      NOT NULL,
    CONSTRAINT [PK_ReturnReceipt] PRIMARY KEY CLUSTERED ([ReturnReceiptId] ASC),
    CONSTRAINT [FK_ReturnReceipt_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);

