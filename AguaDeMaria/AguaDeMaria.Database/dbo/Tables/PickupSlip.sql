CREATE TABLE [dbo].[PickupSlip] (
    [PickupSlipId]     INT            IDENTITY (1, 1) NOT NULL,
    [PickupDate]       DATETIME       NOT NULL,
    [CustomerId]       INT            NOT NULL,
    [Notes]            NVARCHAR (200) NULL,
    [PickupSlipNumber] NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_PickupSlip] PRIMARY KEY CLUSTERED ([PickupSlipId] ASC),
    CONSTRAINT [FK_PickupSlip_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);

