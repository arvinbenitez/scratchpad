
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/08/2014 14:18:42
-- Generated from EDMX file: C:\source\scratchpad\AguaDeMaria\AguaDeMaria.Data\AquaKesh.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AquaKesh];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Customer_CustomerType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_CustomerType];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryReceipt_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryReceipt] DROP CONSTRAINT [FK_DeliveryReceipt_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryReceipt_SalesInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryReceipt] DROP CONSTRAINT [FK_DeliveryReceipt_SalesInvoice];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryReceiptDetail_DeliveryReceipt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryReceiptDetail] DROP CONSTRAINT [FK_DeliveryReceiptDetail_DeliveryReceipt];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryReceiptDetail_ProductType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryReceiptDetail] DROP CONSTRAINT [FK_DeliveryReceiptDetail_ProductType];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetail_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetail_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetail_ProductType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetail_ProductType];
GO
IF OBJECT_ID(N'[dbo].[FK_ReturnReceipt_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReturnReceipt] DROP CONSTRAINT [FK_ReturnReceipt_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_ReturnReceiptDetail_ProductType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReturnReceiptDetail] DROP CONSTRAINT [FK_ReturnReceiptDetail_ProductType];
GO
IF OBJECT_ID(N'[dbo].[FK_ReturnReceiptDetail_ReturnReceipt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReturnReceiptDetail] DROP CONSTRAINT [FK_ReturnReceiptDetail_ReturnReceipt];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesInvoice_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesInvoice] DROP CONSTRAINT [FK_SalesInvoice_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesInvoice_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesInvoice] DROP CONSTRAINT [FK_SalesInvoice_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesInvoiceDetail_ProductType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesInvoiceDetail] DROP CONSTRAINT [FK_SalesInvoiceDetail_ProductType];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesInvoiceDetail_SalesInvoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesInvoiceDetail] DROP CONSTRAINT [FK_SalesInvoiceDetail_SalesInvoice];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[CustomerType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerType];
GO
IF OBJECT_ID(N'[dbo].[DeliveryReceipt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliveryReceipt];
GO
IF OBJECT_ID(N'[dbo].[DeliveryReceiptDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliveryReceiptDetail];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[OrderDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetail];
GO
IF OBJECT_ID(N'[dbo].[ProductType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductType];
GO
IF OBJECT_ID(N'[dbo].[RefStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RefStatus];
GO
IF OBJECT_ID(N'[dbo].[ReturnReceipt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReturnReceipt];
GO
IF OBJECT_ID(N'[dbo].[ReturnReceiptDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReturnReceiptDetail];
GO
IF OBJECT_ID(N'[dbo].[SalesInvoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesInvoice];
GO
IF OBJECT_ID(N'[dbo].[SalesInvoiceDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesInvoiceDetail];
GO
IF OBJECT_ID(N'[dbo].[UserAuth]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAuth];
GO
IF OBJECT_ID(N'[dbo].[UserOAuthProvider]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserOAuthProvider];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerId] int IDENTITY(1,1) NOT NULL,
    [CustomerCode] nvarchar(50)  NULL,
    [CustomerName] nvarchar(100)  NOT NULL,
    [CustomerTypeId] int  NOT NULL,
    [Address] nvarchar(200)  NOT NULL,
    [GPSCoordinate] nvarchar(50)  NULL
);
GO

-- Creating table 'CustomerTypes'
CREATE TABLE [dbo].[CustomerTypes] (
    [CustomerTypeId] int IDENTITY(1,1) NOT NULL,
    [CustomerType1] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'DeliveryReceipts'
CREATE TABLE [dbo].[DeliveryReceipts] (
    [DeliveryReceiptId] int IDENTITY(1,1) NOT NULL,
    [DRNumber] nvarchar(50)  NOT NULL,
    [DRDate] datetime  NOT NULL,
    [CustomerId] int  NOT NULL,
    [SalesInvoiceId] int  NOT NULL
);
GO

-- Creating table 'DeliveryReceiptDetails'
CREATE TABLE [dbo].[DeliveryReceiptDetails] (
    [DeliveryReceiptDetailId] int IDENTITY(1,1) NOT NULL,
    [DeliveryReceiptId] int  NOT NULL,
    [ProductTypeId] int  NOT NULL,
    [Quantity] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [OrderNumber] nvarchar(50)  NOT NULL,
    [CustomerId] int  NOT NULL,
    [OrderDate] datetime  NOT NULL
);
GO

-- Creating table 'OrderDetails'
CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailId] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NOT NULL,
    [ProductTypeId] int  NOT NULL,
    [Qty] int  NOT NULL
);
GO

-- Creating table 'ProductTypes'
CREATE TABLE [dbo].[ProductTypes] (
    [ProductTypeId] int IDENTITY(1,1) NOT NULL,
    [ProductType1] nvarchar(100)  NOT NULL,
    [Description] nvarchar(100)  NULL,
    [BasePrice] decimal(9,2)  NOT NULL,
    [IsInvetoriable] int  NOT NULL
);
GO

-- Creating table 'RefStatus'
CREATE TABLE [dbo].[RefStatus] (
    [StatusId] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'ReturnReceipts'
CREATE TABLE [dbo].[ReturnReceipts] (
    [ReturnReceiptId] int IDENTITY(1,1) NOT NULL,
    [ReturnReceiptDate] datetime  NOT NULL,
    [CustomerId] int  NOT NULL
);
GO

-- Creating table 'ReturnReceiptDetails'
CREATE TABLE [dbo].[ReturnReceiptDetails] (
    [ReturnReceiptId] int  NOT NULL,
    [ReturnReceiptDetailId] int IDENTITY(1,1) NOT NULL,
    [ProductTypeId] int  NOT NULL,
    [Quantity] decimal(9,2)  NOT NULL
);
GO

-- Creating table 'SalesInvoices'
CREATE TABLE [dbo].[SalesInvoices] (
    [SalesInvoiceId] int IDENTITY(1,1) NOT NULL,
    [InvoiceNumber] nvarchar(50)  NOT NULL,
    [InvoiceDate] datetime  NOT NULL,
    [CustomerId] int  NOT NULL,
    [Remarks] nvarchar(200)  NULL,
    [OrderId] int  NULL
);
GO

-- Creating table 'SalesInvoiceDetails'
CREATE TABLE [dbo].[SalesInvoiceDetails] (
    [SalesInvoiceDetailId] int IDENTITY(1,1) NOT NULL,
    [SalesInvoiceId] int  NOT NULL,
    [ProductTypeId] int  NOT NULL,
    [Quantity] decimal(9,2)  NOT NULL,
    [Price] decimal(9,2)  NOT NULL
);
GO

-- Creating table 'UserAuths'
CREATE TABLE [dbo].[UserAuths] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(8000)  NULL,
    [Email] varchar(8000)  NULL,
    [PrimaryEmail] varchar(8000)  NULL,
    [FirstName] varchar(8000)  NULL,
    [LastName] varchar(8000)  NULL,
    [DisplayName] varchar(8000)  NULL,
    [BirthDate] datetime  NULL,
    [BirthDateRaw] varchar(8000)  NULL,
    [Country] varchar(8000)  NULL,
    [Culture] varchar(8000)  NULL,
    [FullName] varchar(8000)  NULL,
    [Gender] varchar(8000)  NULL,
    [Language] varchar(8000)  NULL,
    [MailAddress] varchar(8000)  NULL,
    [Nickname] varchar(8000)  NULL,
    [PostalCode] varchar(8000)  NULL,
    [TimeZone] varchar(8000)  NULL,
    [Salt] varchar(8000)  NULL,
    [PasswordHash] varchar(8000)  NULL,
    [DigestHa1Hash] varchar(8000)  NULL,
    [Roles] varchar(8000)  NULL,
    [Permissions] varchar(8000)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [RefId] int  NULL,
    [RefIdStr] varchar(8000)  NULL,
    [Meta] varchar(8000)  NULL
);
GO

-- Creating table 'UserOAuthProviders'
CREATE TABLE [dbo].[UserOAuthProviders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserAuthId] int  NOT NULL,
    [Provider] varchar(8000)  NULL,
    [UserId] varchar(8000)  NULL,
    [UserName] varchar(8000)  NULL,
    [FullName] varchar(8000)  NULL,
    [DisplayName] varchar(8000)  NULL,
    [FirstName] varchar(8000)  NULL,
    [LastName] varchar(8000)  NULL,
    [Email] varchar(8000)  NULL,
    [BirthDate] datetime  NULL,
    [BirthDateRaw] varchar(8000)  NULL,
    [Country] varchar(8000)  NULL,
    [Culture] varchar(8000)  NULL,
    [Gender] varchar(8000)  NULL,
    [Language] varchar(8000)  NULL,
    [MailAddress] varchar(8000)  NULL,
    [Nickname] varchar(8000)  NULL,
    [PostalCode] varchar(8000)  NULL,
    [TimeZone] varchar(8000)  NULL,
    [RefreshToken] varchar(8000)  NULL,
    [RefreshTokenExpiry] datetime  NULL,
    [RequestToken] varchar(8000)  NULL,
    [RequestTokenSecret] varchar(8000)  NULL,
    [Items] varchar(8000)  NULL,
    [AccessToken] varchar(8000)  NULL,
    [AccessTokenSecret] varchar(8000)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [RefId] int  NULL,
    [RefIdStr] varchar(8000)  NULL,
    [Meta] varchar(8000)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CustomerId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerId] ASC);
GO

-- Creating primary key on [CustomerTypeId] in table 'CustomerTypes'
ALTER TABLE [dbo].[CustomerTypes]
ADD CONSTRAINT [PK_CustomerTypes]
    PRIMARY KEY CLUSTERED ([CustomerTypeId] ASC);
GO

-- Creating primary key on [DeliveryReceiptId] in table 'DeliveryReceipts'
ALTER TABLE [dbo].[DeliveryReceipts]
ADD CONSTRAINT [PK_DeliveryReceipts]
    PRIMARY KEY CLUSTERED ([DeliveryReceiptId] ASC);
GO

-- Creating primary key on [DeliveryReceiptDetailId] in table 'DeliveryReceiptDetails'
ALTER TABLE [dbo].[DeliveryReceiptDetails]
ADD CONSTRAINT [PK_DeliveryReceiptDetails]
    PRIMARY KEY CLUSTERED ([DeliveryReceiptDetailId] ASC);
GO

-- Creating primary key on [OrderId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- Creating primary key on [OrderDetailId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [PK_OrderDetails]
    PRIMARY KEY CLUSTERED ([OrderDetailId] ASC);
GO

-- Creating primary key on [ProductTypeId] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [PK_ProductTypes]
    PRIMARY KEY CLUSTERED ([ProductTypeId] ASC);
GO

-- Creating primary key on [StatusId] in table 'RefStatus'
ALTER TABLE [dbo].[RefStatus]
ADD CONSTRAINT [PK_RefStatus]
    PRIMARY KEY CLUSTERED ([StatusId] ASC);
GO

-- Creating primary key on [ReturnReceiptId] in table 'ReturnReceipts'
ALTER TABLE [dbo].[ReturnReceipts]
ADD CONSTRAINT [PK_ReturnReceipts]
    PRIMARY KEY CLUSTERED ([ReturnReceiptId] ASC);
GO

-- Creating primary key on [ReturnReceiptDetailId] in table 'ReturnReceiptDetails'
ALTER TABLE [dbo].[ReturnReceiptDetails]
ADD CONSTRAINT [PK_ReturnReceiptDetails]
    PRIMARY KEY CLUSTERED ([ReturnReceiptDetailId] ASC);
GO

-- Creating primary key on [SalesInvoiceId] in table 'SalesInvoices'
ALTER TABLE [dbo].[SalesInvoices]
ADD CONSTRAINT [PK_SalesInvoices]
    PRIMARY KEY CLUSTERED ([SalesInvoiceId] ASC);
GO

-- Creating primary key on [SalesInvoiceDetailId] in table 'SalesInvoiceDetails'
ALTER TABLE [dbo].[SalesInvoiceDetails]
ADD CONSTRAINT [PK_SalesInvoiceDetails]
    PRIMARY KEY CLUSTERED ([SalesInvoiceDetailId] ASC);
GO

-- Creating primary key on [Id] in table 'UserAuths'
ALTER TABLE [dbo].[UserAuths]
ADD CONSTRAINT [PK_UserAuths]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserOAuthProviders'
ALTER TABLE [dbo].[UserOAuthProviders]
ADD CONSTRAINT [PK_UserOAuthProviders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerTypeId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_Customer_CustomerType]
    FOREIGN KEY ([CustomerTypeId])
    REFERENCES [dbo].[CustomerTypes]
        ([CustomerTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Customer_CustomerType'
CREATE INDEX [IX_FK_Customer_CustomerType]
ON [dbo].[Customers]
    ([CustomerTypeId]);
GO

-- Creating foreign key on [CustomerId] in table 'DeliveryReceipts'
ALTER TABLE [dbo].[DeliveryReceipts]
ADD CONSTRAINT [FK_DeliveryReceipt_Customer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryReceipt_Customer'
CREATE INDEX [IX_FK_DeliveryReceipt_Customer]
ON [dbo].[DeliveryReceipts]
    ([CustomerId]);
GO

-- Creating foreign key on [CustomerId] in table 'ReturnReceipts'
ALTER TABLE [dbo].[ReturnReceipts]
ADD CONSTRAINT [FK_ReturnReceipt_Customer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReturnReceipt_Customer'
CREATE INDEX [IX_FK_ReturnReceipt_Customer]
ON [dbo].[ReturnReceipts]
    ([CustomerId]);
GO

-- Creating foreign key on [CustomerId] in table 'SalesInvoices'
ALTER TABLE [dbo].[SalesInvoices]
ADD CONSTRAINT [FK_SalesInvoice_Customer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesInvoice_Customer'
CREATE INDEX [IX_FK_SalesInvoice_Customer]
ON [dbo].[SalesInvoices]
    ([CustomerId]);
GO

-- Creating foreign key on [SalesInvoiceId] in table 'DeliveryReceipts'
ALTER TABLE [dbo].[DeliveryReceipts]
ADD CONSTRAINT [FK_DeliveryReceipt_SalesInvoice]
    FOREIGN KEY ([SalesInvoiceId])
    REFERENCES [dbo].[SalesInvoices]
        ([SalesInvoiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryReceipt_SalesInvoice'
CREATE INDEX [IX_FK_DeliveryReceipt_SalesInvoice]
ON [dbo].[DeliveryReceipts]
    ([SalesInvoiceId]);
GO

-- Creating foreign key on [DeliveryReceiptId] in table 'DeliveryReceiptDetails'
ALTER TABLE [dbo].[DeliveryReceiptDetails]
ADD CONSTRAINT [FK_DeliveryReceiptDetail_DeliveryReceipt]
    FOREIGN KEY ([DeliveryReceiptId])
    REFERENCES [dbo].[DeliveryReceipts]
        ([DeliveryReceiptId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryReceiptDetail_DeliveryReceipt'
CREATE INDEX [IX_FK_DeliveryReceiptDetail_DeliveryReceipt]
ON [dbo].[DeliveryReceiptDetails]
    ([DeliveryReceiptId]);
GO

-- Creating foreign key on [ProductTypeId] in table 'DeliveryReceiptDetails'
ALTER TABLE [dbo].[DeliveryReceiptDetails]
ADD CONSTRAINT [FK_DeliveryReceiptDetail_ProductType]
    FOREIGN KEY ([ProductTypeId])
    REFERENCES [dbo].[ProductTypes]
        ([ProductTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryReceiptDetail_ProductType'
CREATE INDEX [IX_FK_DeliveryReceiptDetail_ProductType]
ON [dbo].[DeliveryReceiptDetails]
    ([ProductTypeId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetail_Order]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([OrderId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_Order'
CREATE INDEX [IX_FK_OrderDetail_Order]
ON [dbo].[OrderDetails]
    ([OrderId]);
GO

-- Creating foreign key on [OrderId] in table 'SalesInvoices'
ALTER TABLE [dbo].[SalesInvoices]
ADD CONSTRAINT [FK_SalesInvoice_Order]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([OrderId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesInvoice_Order'
CREATE INDEX [IX_FK_SalesInvoice_Order]
ON [dbo].[SalesInvoices]
    ([OrderId]);
GO

-- Creating foreign key on [ProductTypeId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetail_ProductType]
    FOREIGN KEY ([ProductTypeId])
    REFERENCES [dbo].[ProductTypes]
        ([ProductTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_ProductType'
CREATE INDEX [IX_FK_OrderDetail_ProductType]
ON [dbo].[OrderDetails]
    ([ProductTypeId]);
GO

-- Creating foreign key on [ProductTypeId] in table 'ReturnReceiptDetails'
ALTER TABLE [dbo].[ReturnReceiptDetails]
ADD CONSTRAINT [FK_ReturnReceiptDetail_ProductType]
    FOREIGN KEY ([ProductTypeId])
    REFERENCES [dbo].[ProductTypes]
        ([ProductTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReturnReceiptDetail_ProductType'
CREATE INDEX [IX_FK_ReturnReceiptDetail_ProductType]
ON [dbo].[ReturnReceiptDetails]
    ([ProductTypeId]);
GO

-- Creating foreign key on [ProductTypeId] in table 'SalesInvoiceDetails'
ALTER TABLE [dbo].[SalesInvoiceDetails]
ADD CONSTRAINT [FK_SalesInvoiceDetail_ProductType]
    FOREIGN KEY ([ProductTypeId])
    REFERENCES [dbo].[ProductTypes]
        ([ProductTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesInvoiceDetail_ProductType'
CREATE INDEX [IX_FK_SalesInvoiceDetail_ProductType]
ON [dbo].[SalesInvoiceDetails]
    ([ProductTypeId]);
GO

-- Creating foreign key on [ReturnReceiptId] in table 'ReturnReceiptDetails'
ALTER TABLE [dbo].[ReturnReceiptDetails]
ADD CONSTRAINT [FK_ReturnReceiptDetail_ReturnReceipt]
    FOREIGN KEY ([ReturnReceiptId])
    REFERENCES [dbo].[ReturnReceipts]
        ([ReturnReceiptId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReturnReceiptDetail_ReturnReceipt'
CREATE INDEX [IX_FK_ReturnReceiptDetail_ReturnReceipt]
ON [dbo].[ReturnReceiptDetails]
    ([ReturnReceiptId]);
GO

-- Creating foreign key on [SalesInvoiceId] in table 'SalesInvoiceDetails'
ALTER TABLE [dbo].[SalesInvoiceDetails]
ADD CONSTRAINT [FK_SalesInvoiceDetail_SalesInvoice]
    FOREIGN KEY ([SalesInvoiceId])
    REFERENCES [dbo].[SalesInvoices]
        ([SalesInvoiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesInvoiceDetail_SalesInvoice'
CREATE INDEX [IX_FK_SalesInvoiceDetail_SalesInvoice]
ON [dbo].[SalesInvoiceDetails]
    ([SalesInvoiceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------