CREATE TABLE [dbo].[PickupSlipDetail] (
    [PickupSlipDetailId] INT IDENTITY (1, 1) NOT NULL,
    [PickupSlipId]       INT NOT NULL,
    [ProductTypeId]      INT NOT NULL,
    [Quantity]           INT NOT NULL,
    CONSTRAINT [PK_PickupSlipDetail] PRIMARY KEY CLUSTERED ([PickupSlipDetailId] ASC),
    CONSTRAINT [FK_PickupSlipDetail_PickupSlip] FOREIGN KEY ([PickupSlipId]) REFERENCES [dbo].[PickupSlip] ([PickupSlipId]),
    CONSTRAINT [FK_PickupSlipDetail_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[ProductType] ([ProductTypeId])
);

