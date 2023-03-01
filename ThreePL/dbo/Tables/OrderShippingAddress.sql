CREATE TABLE [dbo].[OrderShippingAddress]
(
	[ShippingId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderHeaderId] INT NOT NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [StreetAddress] VARCHAR(MAX) NOT NULL, 
    [AddressBox] VARCHAR(50) NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [State] VARCHAR(50) NOT NULL, 
    [ZipCode] VARCHAR(50) NOT NULL, 
    [Country] VARBINARY(50) NULL, 
    CONSTRAINT [FK_CustomerInfo_OrderHeader] FOREIGN KEY ([OrderHeaderId]) REFERENCES [OrderHeader](HeaderId)
)

GO

CREATE INDEX [IX_CustomerInfo_OrderHeaderId] ON [dbo].[OrderShippingAddress] ([OrderHeaderId])

GO

CREATE INDEX [IX_CustomerInfo_CustomerId] ON [dbo].[OrderShippingAddress] ([ShippingId])
