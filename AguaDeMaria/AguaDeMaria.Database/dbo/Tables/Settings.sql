CREATE TABLE [dbo].[Settings] (
    [SettingId]                    INT IDENTITY (1, 1) NOT NULL,
    [OrderNumberCounter]           INT NOT NULL,
    [DeliveryReceiptNumberCounter] INT NOT NULL,
    [PickupSlipNumberCounter]      INT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([SettingId] ASC)
);

