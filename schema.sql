IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [DiscountTypes] (
    [IdDiscountType] int NOT NULL IDENTITY,
    [DiscountTypeName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_DiscountTypes] PRIMARY KEY ([IdDiscountType])
);
GO

CREATE TABLE [OrderStatuses] (
    [IdOrderStatus] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_OrderStatuses] PRIMARY KEY ([IdOrderStatus])
);
GO

CREATE TABLE [PaymentMethods] (
    [IdPaymentMethod] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PaymentMethods] PRIMARY KEY ([IdPaymentMethod])
);
GO

CREATE TABLE [PaymentStatuses] (
    [IdPaymentStatus] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PaymentStatuses] PRIMARY KEY ([IdPaymentStatus])
);
GO

CREATE TABLE [ProductCategories] (
    [IdProductCategory] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ProductCategories] PRIMARY KEY ([IdProductCategory])
);
GO

CREATE TABLE [Suppliers] (
    [IdSupplier] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [ContactEmail] nvarchar(max) NOT NULL,
    [PostCode] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [StreetAndNumber] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY ([IdSupplier])
);
GO

CREATE TABLE [UserRoles] (
    [IdUserRole] int NOT NULL IDENTITY,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([IdUserRole])
);
GO

CREATE TABLE [Promotions] (
    [IdPromotion] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [IdDiscountType] int NOT NULL,
    [DiscountValue] decimal(18,2) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Promotions] PRIMARY KEY ([IdPromotion]),
    CONSTRAINT [FK_Promotions_DiscountTypes_IdDiscountType] FOREIGN KEY ([IdDiscountType]) REFERENCES [DiscountTypes] ([IdDiscountType]) ON DELETE CASCADE
);
GO

CREATE TABLE [Products] (
    [IdProduct] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [IdProductCategory] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [SupplierId] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([IdProduct]),
    CONSTRAINT [FK_Products_ProductCategories_IdProductCategory] FOREIGN KEY ([IdProductCategory]) REFERENCES [ProductCategories] ([IdProductCategory]) ON DELETE CASCADE,
    CONSTRAINT [FK_Products_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([IdSupplier]) ON DELETE CASCADE
);
GO

CREATE TABLE [Users] (
    [IdUser] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [PostCode] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [StreetAndNumber] nvarchar(max) NOT NULL,
    [IdUserRole] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([IdUser]),
    CONSTRAINT [FK_Users_UserRoles_IdUserRole] FOREIGN KEY ([IdUserRole]) REFERENCES [UserRoles] ([IdUserRole]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductImages] (
    [IdProductImage] int NOT NULL IDENTITY,
    [IdProduct] int NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ProductImages] PRIMARY KEY ([IdProductImage]),
    CONSTRAINT [FK_ProductImages_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductPromotions] (
    [IdProduct] int NOT NULL,
    [IdPromotion] int NOT NULL,
    CONSTRAINT [PK_ProductPromotions] PRIMARY KEY ([IdProduct], [IdPromotion]),
    CONSTRAINT [FK_ProductPromotions_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductPromotions_Promotions_IdPromotion] FOREIGN KEY ([IdPromotion]) REFERENCES [Promotions] ([IdPromotion]) ON DELETE CASCADE
);
GO

CREATE TABLE [Stocks] (
    [IdStock] int NOT NULL IDENTITY,
    [IdProduct] int NOT NULL,
    [Quantity] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Stocks] PRIMARY KEY ([IdStock]),
    CONSTRAINT [FK_Stocks_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]) ON DELETE CASCADE
);
GO

CREATE TABLE [HockeyNews] (
    [IdHockeyNews] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [IdAuthor] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [Published] bit NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_HockeyNews] PRIMARY KEY ([IdHockeyNews]),
    CONSTRAINT [FK_HockeyNews_Users_IdAuthor] FOREIGN KEY ([IdAuthor]) REFERENCES [Users] ([IdUser]) ON DELETE CASCADE
);
GO

CREATE TABLE [Orders] (
    [IdOrder] int NOT NULL IDENTITY,
    [OrderDate] datetime2 NOT NULL,
    [IdUser] int NOT NULL,
    [IdOrderStatus] int NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([IdOrder]),
    CONSTRAINT [FK_Orders_OrderStatuses_IdOrderStatus] FOREIGN KEY ([IdOrderStatus]) REFERENCES [OrderStatuses] ([IdOrderStatus]),
    CONSTRAINT [FK_Orders_Users_IdUser] FOREIGN KEY ([IdUser]) REFERENCES [Users] ([IdUser]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserCarts] (
    [IdUserCart] int NOT NULL IDENTITY,
    [IdUser] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_UserCarts] PRIMARY KEY ([IdUserCart]),
    CONSTRAINT [FK_UserCarts_Users_IdUser] FOREIGN KEY ([IdUser]) REFERENCES [Users] ([IdUser]) ON DELETE CASCADE
);
GO

CREATE TABLE [Invoices] (
    [IdInvoice] int NOT NULL IDENTITY,
    [IdOrder] int NOT NULL,
    [InvoiceNumber] nvarchar(max) NOT NULL,
    [IssueDate] datetime2 NOT NULL,
    [TotalAmount] decimal(18,2) NOT NULL,
    [IdUser] int NOT NULL,
    [UserIdUser] int NULL,
    CONSTRAINT [PK_Invoices] PRIMARY KEY ([IdInvoice]),
    CONSTRAINT [FK_Invoices_Orders_IdOrder] FOREIGN KEY ([IdOrder]) REFERENCES [Orders] ([IdOrder]) ON DELETE CASCADE,
    CONSTRAINT [FK_Invoices_Users_IdUser] FOREIGN KEY ([IdUser]) REFERENCES [Users] ([IdUser]),
    CONSTRAINT [FK_Invoices_Users_UserIdUser] FOREIGN KEY ([UserIdUser]) REFERENCES [Users] ([IdUser])
);
GO

CREATE TABLE [OrderItems] (
    [IdOrderItem] int NOT NULL IDENTITY,
    [IdOrder] int NOT NULL,
    [IdProduct] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([IdOrderItem]),
    CONSTRAINT [FK_OrderItems_Orders_IdOrder] FOREIGN KEY ([IdOrder]) REFERENCES [Orders] ([IdOrder]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]) ON DELETE CASCADE
);
GO

CREATE TABLE [Payments] (
    [IdPayment] int NOT NULL IDENTITY,
    [IdOrder] int NOT NULL,
    [PaymentDate] datetime2 NOT NULL,
    [IdPaymentMethod] int NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [IdPaymentStatus] int NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([IdPayment]),
    CONSTRAINT [FK_Payments_Orders_IdOrder] FOREIGN KEY ([IdOrder]) REFERENCES [Orders] ([IdOrder]) ON DELETE CASCADE,
    CONSTRAINT [FK_Payments_PaymentMethods_IdPaymentMethod] FOREIGN KEY ([IdPaymentMethod]) REFERENCES [PaymentMethods] ([IdPaymentMethod]) ON DELETE CASCADE,
    CONSTRAINT [FK_Payments_PaymentStatuses_IdPaymentStatus] FOREIGN KEY ([IdPaymentStatus]) REFERENCES [PaymentStatuses] ([IdPaymentStatus]) ON DELETE CASCADE
);
GO

CREATE TABLE [CartItems] (
    [IdCartItem] int NOT NULL IDENTITY,
    [IdUserCart] int NOT NULL,
    [IdProduct] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_CartItems] PRIMARY KEY ([IdCartItem]),
    CONSTRAINT [FK_CartItems_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartItems_UserCarts_IdUserCart] FOREIGN KEY ([IdUserCart]) REFERENCES [UserCarts] ([IdUserCart]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CartItems_IdProduct] ON [CartItems] ([IdProduct]);
GO

CREATE INDEX [IX_CartItems_IdUserCart] ON [CartItems] ([IdUserCart]);
GO

CREATE INDEX [IX_HockeyNews_IdAuthor] ON [HockeyNews] ([IdAuthor]);
GO

CREATE UNIQUE INDEX [IX_Invoices_IdOrder] ON [Invoices] ([IdOrder]);
GO

CREATE INDEX [IX_Invoices_IdUser] ON [Invoices] ([IdUser]);
GO

CREATE INDEX [IX_Invoices_UserIdUser] ON [Invoices] ([UserIdUser]);
GO

CREATE INDEX [IX_OrderItems_IdOrder] ON [OrderItems] ([IdOrder]);
GO

CREATE INDEX [IX_OrderItems_IdProduct] ON [OrderItems] ([IdProduct]);
GO

CREATE INDEX [IX_Orders_IdOrderStatus] ON [Orders] ([IdOrderStatus]);
GO

CREATE INDEX [IX_Orders_IdUser] ON [Orders] ([IdUser]);
GO

CREATE UNIQUE INDEX [IX_Payments_IdOrder] ON [Payments] ([IdOrder]);
GO

CREATE INDEX [IX_Payments_IdPaymentMethod] ON [Payments] ([IdPaymentMethod]);
GO

CREATE INDEX [IX_Payments_IdPaymentStatus] ON [Payments] ([IdPaymentStatus]);
GO

CREATE INDEX [IX_ProductImages_IdProduct] ON [ProductImages] ([IdProduct]);
GO

CREATE INDEX [IX_ProductPromotions_IdPromotion] ON [ProductPromotions] ([IdPromotion]);
GO

CREATE INDEX [IX_Products_IdProductCategory] ON [Products] ([IdProductCategory]);
GO

CREATE INDEX [IX_Products_SupplierId] ON [Products] ([SupplierId]);
GO

CREATE INDEX [IX_Promotions_IdDiscountType] ON [Promotions] ([IdDiscountType]);
GO

CREATE INDEX [IX_Stocks_IdProduct] ON [Stocks] ([IdProduct]);
GO

CREATE INDEX [IX_UserCarts_IdUser] ON [UserCarts] ([IdUser]);
GO

CREATE INDEX [IX_Users_IdUserRole] ON [Users] ([IdUserRole]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250508121632_M1', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Suppliers_SupplierId];
GO

EXEC sp_rename N'[Products].[SupplierId]', N'IdSupplier', N'COLUMN';
GO

EXEC sp_rename N'[Products].[IX_Products_SupplierId]', N'IX_Products_IdSupplier', N'INDEX';
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Suppliers_IdSupplier] FOREIGN KEY ([IdSupplier]) REFERENCES [Suppliers] ([IdSupplier]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250511222050_RenameSupplierIdToIdSupplier', N'8.0.11');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CartItems] DROP CONSTRAINT [FK_CartItems_Products_IdProduct];
GO

ALTER TABLE [CartItems] DROP CONSTRAINT [FK_CartItems_UserCarts_IdUserCart];
GO

ALTER TABLE [OrderItems] DROP CONSTRAINT [FK_OrderItems_Orders_IdOrder];
GO

ALTER TABLE [OrderItems] DROP CONSTRAINT [FK_OrderItems_Products_IdProduct];
GO

ALTER TABLE [Orders] DROP CONSTRAINT [FK_Orders_Users_IdUser];
GO

ALTER TABLE [Payments] DROP CONSTRAINT [FK_Payments_Orders_IdOrder];
GO

ALTER TABLE [Payments] DROP CONSTRAINT [FK_Payments_PaymentMethods_IdPaymentMethod];
GO

ALTER TABLE [Payments] DROP CONSTRAINT [FK_Payments_PaymentStatuses_IdPaymentStatus];
GO

ALTER TABLE [ProductPromotions] DROP CONSTRAINT [FK_ProductPromotions_Products_IdProduct];
GO

ALTER TABLE [ProductPromotions] DROP CONSTRAINT [FK_ProductPromotions_Promotions_IdPromotion];
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_ProductCategories_IdProductCategory];
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Suppliers_IdSupplier];
GO

ALTER TABLE [Stocks] DROP CONSTRAINT [FK_Stocks_Products_IdProduct];
GO

ALTER TABLE [UserCarts] DROP CONSTRAINT [FK_UserCarts_Users_IdUser];
GO

ALTER TABLE [Users] DROP CONSTRAINT [FK_Users_UserRoles_IdUserRole];
GO

DROP INDEX [IX_Payments_IdOrder] ON [Payments];
GO

DROP INDEX [IX_Invoices_IdOrder] ON [Invoices];
GO

ALTER TABLE [Users] ADD [UserRoleIdUserRole] int NULL;
GO

ALTER TABLE [UserCarts] ADD [UserIdUser] int NULL;
GO

ALTER TABLE [Stocks] ADD [ProductIdProduct] int NULL;
GO

ALTER TABLE [Products] ADD [ProductCategoryIdProductCategory] int NULL;
GO

ALTER TABLE [Products] ADD [SupplierIdSupplier] int NULL;
GO

ALTER TABLE [Payments] ADD [OrderIdOrder] int NULL;
GO

ALTER TABLE [Payments] ADD [PaymentMethodIdPaymentMethod] int NULL;
GO

ALTER TABLE [Payments] ADD [PaymentStatusIdPaymentStatus] int NULL;
GO

ALTER TABLE [Orders] ADD [OrderStatusIdOrderStatus] int NULL;
GO

ALTER TABLE [Orders] ADD [UserIdUser] int NULL;
GO

ALTER TABLE [OrderItems] ADD [OrderIdOrder] int NULL;
GO

ALTER TABLE [OrderItems] ADD [ProductIdProduct] int NULL;
GO

ALTER TABLE [CartItems] ADD [ProductIdProduct] int NULL;
GO

ALTER TABLE [CartItems] ADD [UserCartIdUserCart] int NULL;
GO

CREATE INDEX [IX_Users_UserRoleIdUserRole] ON [Users] ([UserRoleIdUserRole]);
GO

CREATE INDEX [IX_UserCarts_UserIdUser] ON [UserCarts] ([UserIdUser]);
GO

CREATE INDEX [IX_Stocks_ProductIdProduct] ON [Stocks] ([ProductIdProduct]);
GO

CREATE INDEX [IX_Products_ProductCategoryIdProductCategory] ON [Products] ([ProductCategoryIdProductCategory]);
GO

CREATE INDEX [IX_Products_SupplierIdSupplier] ON [Products] ([SupplierIdSupplier]);
GO

CREATE INDEX [IX_Payments_IdOrder] ON [Payments] ([IdOrder]);
GO

CREATE INDEX [IX_Payments_OrderIdOrder] ON [Payments] ([OrderIdOrder]);
GO

CREATE INDEX [IX_Payments_PaymentMethodIdPaymentMethod] ON [Payments] ([PaymentMethodIdPaymentMethod]);
GO

CREATE INDEX [IX_Payments_PaymentStatusIdPaymentStatus] ON [Payments] ([PaymentStatusIdPaymentStatus]);
GO

CREATE INDEX [IX_Orders_OrderStatusIdOrderStatus] ON [Orders] ([OrderStatusIdOrderStatus]);
GO

CREATE INDEX [IX_Orders_UserIdUser] ON [Orders] ([UserIdUser]);
GO

CREATE INDEX [IX_OrderItems_OrderIdOrder] ON [OrderItems] ([OrderIdOrder]);
GO

CREATE INDEX [IX_OrderItems_ProductIdProduct] ON [OrderItems] ([ProductIdProduct]);
GO

CREATE INDEX [IX_Invoices_IdOrder] ON [Invoices] ([IdOrder]);
GO

CREATE INDEX [IX_CartItems_ProductIdProduct] ON [CartItems] ([ProductIdProduct]);
GO

CREATE INDEX [IX_CartItems_UserCartIdUserCart] ON [CartItems] ([UserCartIdUserCart]);
GO

ALTER TABLE [CartItems] ADD CONSTRAINT [FK_CartItems_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]);
GO

ALTER TABLE [CartItems] ADD CONSTRAINT [FK_CartItems_Products_ProductIdProduct] FOREIGN KEY ([ProductIdProduct]) REFERENCES [Products] ([IdProduct]);
GO

ALTER TABLE [CartItems] ADD CONSTRAINT [FK_CartItems_UserCarts_IdUserCart] FOREIGN KEY ([IdUserCart]) REFERENCES [UserCarts] ([IdUserCart]);
GO

ALTER TABLE [CartItems] ADD CONSTRAINT [FK_CartItems_UserCarts_UserCartIdUserCart] FOREIGN KEY ([UserCartIdUserCart]) REFERENCES [UserCarts] ([IdUserCart]);
GO

ALTER TABLE [OrderItems] ADD CONSTRAINT [FK_OrderItems_Orders_IdOrder] FOREIGN KEY ([IdOrder]) REFERENCES [Orders] ([IdOrder]);
GO

ALTER TABLE [OrderItems] ADD CONSTRAINT [FK_OrderItems_Orders_OrderIdOrder] FOREIGN KEY ([OrderIdOrder]) REFERENCES [Orders] ([IdOrder]);
GO

ALTER TABLE [OrderItems] ADD CONSTRAINT [FK_OrderItems_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]);
GO

ALTER TABLE [OrderItems] ADD CONSTRAINT [FK_OrderItems_Products_ProductIdProduct] FOREIGN KEY ([ProductIdProduct]) REFERENCES [Products] ([IdProduct]);
GO

ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_OrderStatuses_OrderStatusIdOrderStatus] FOREIGN KEY ([OrderStatusIdOrderStatus]) REFERENCES [OrderStatuses] ([IdOrderStatus]);
GO

ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Users_IdUser] FOREIGN KEY ([IdUser]) REFERENCES [Users] ([IdUser]);
GO

ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Users_UserIdUser] FOREIGN KEY ([UserIdUser]) REFERENCES [Users] ([IdUser]);
GO

ALTER TABLE [Payments] ADD CONSTRAINT [FK_Payments_Orders_IdOrder] FOREIGN KEY ([IdOrder]) REFERENCES [Orders] ([IdOrder]);
GO

ALTER TABLE [Payments] ADD CONSTRAINT [FK_Payments_Orders_OrderIdOrder] FOREIGN KEY ([OrderIdOrder]) REFERENCES [Orders] ([IdOrder]);
GO

ALTER TABLE [Payments] ADD CONSTRAINT [FK_Payments_PaymentMethods_IdPaymentMethod] FOREIGN KEY ([IdPaymentMethod]) REFERENCES [PaymentMethods] ([IdPaymentMethod]);
GO

ALTER TABLE [Payments] ADD CONSTRAINT [FK_Payments_PaymentMethods_PaymentMethodIdPaymentMethod] FOREIGN KEY ([PaymentMethodIdPaymentMethod]) REFERENCES [PaymentMethods] ([IdPaymentMethod]);
GO

ALTER TABLE [Payments] ADD CONSTRAINT [FK_Payments_PaymentStatuses_IdPaymentStatus] FOREIGN KEY ([IdPaymentStatus]) REFERENCES [PaymentStatuses] ([IdPaymentStatus]);
GO

ALTER TABLE [Payments] ADD CONSTRAINT [FK_Payments_PaymentStatuses_PaymentStatusIdPaymentStatus] FOREIGN KEY ([PaymentStatusIdPaymentStatus]) REFERENCES [PaymentStatuses] ([IdPaymentStatus]);
GO

ALTER TABLE [ProductPromotions] ADD CONSTRAINT [FK_ProductPromotions_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]);
GO

ALTER TABLE [ProductPromotions] ADD CONSTRAINT [FK_ProductPromotions_Promotions_IdPromotion] FOREIGN KEY ([IdPromotion]) REFERENCES [Promotions] ([IdPromotion]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ProductCategories_IdProductCategory] FOREIGN KEY ([IdProductCategory]) REFERENCES [ProductCategories] ([IdProductCategory]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ProductCategories_ProductCategoryIdProductCategory] FOREIGN KEY ([ProductCategoryIdProductCategory]) REFERENCES [ProductCategories] ([IdProductCategory]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Suppliers_IdSupplier] FOREIGN KEY ([IdSupplier]) REFERENCES [Suppliers] ([IdSupplier]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Suppliers_SupplierIdSupplier] FOREIGN KEY ([SupplierIdSupplier]) REFERENCES [Suppliers] ([IdSupplier]);
GO

ALTER TABLE [Stocks] ADD CONSTRAINT [FK_Stocks_Products_IdProduct] FOREIGN KEY ([IdProduct]) REFERENCES [Products] ([IdProduct]);
GO

ALTER TABLE [Stocks] ADD CONSTRAINT [FK_Stocks_Products_ProductIdProduct] FOREIGN KEY ([ProductIdProduct]) REFERENCES [Products] ([IdProduct]);
GO

ALTER TABLE [UserCarts] ADD CONSTRAINT [FK_UserCarts_Users_IdUser] FOREIGN KEY ([IdUser]) REFERENCES [Users] ([IdUser]);
GO

ALTER TABLE [UserCarts] ADD CONSTRAINT [FK_UserCarts_Users_UserIdUser] FOREIGN KEY ([UserIdUser]) REFERENCES [Users] ([IdUser]);
GO

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_UserRoles_IdUserRole] FOREIGN KEY ([IdUserRole]) REFERENCES [UserRoles] ([IdUserRole]);
GO

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_UserRoles_UserRoleIdUserRole] FOREIGN KEY ([UserRoleIdUserRole]) REFERENCES [UserRoles] ([IdUserRole]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250512093637_UpdateDeleteBehavior', N'8.0.11');
GO

COMMIT;
GO

