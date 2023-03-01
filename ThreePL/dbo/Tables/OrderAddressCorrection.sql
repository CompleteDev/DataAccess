CREATE TABLE [dbo].[OrderAddressCorrection]
(
	[CorrectionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ShippingId] INT NOT NULL, 
    [StreetAddress] VARCHAR(MAX) NOT NULL, 
    [BoxAddress] VARCHAR(50) NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [State] VARCHAR(50) NOT NULL, 
    [ZipCode] VARCHAR(50) NOT NULL, 
    [CorrectionUsed] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_OrderAddressCorrection_OrderShipping] FOREIGN KEY ([ShippingId]) REFERENCES [OrderShippingAddress]([ShippingId])
)

GO

CREATE INDEX [IX_OrderAddressCorrection_ShippingId] ON [dbo].[OrderAddressCorrection] ([ShippingId])
