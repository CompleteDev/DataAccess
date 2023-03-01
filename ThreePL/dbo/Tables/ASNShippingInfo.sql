CREATE TABLE [dbo].[ASNShippingInfo]
(
	[ASNShippingId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ASNHeaderId] INT NOT NULL, 
    [CarrierId] INT NOT NULL, 
    [ShipMethodId] INT NOT NULL, 
    CONSTRAINT [FK_ASNShippingInfo_HeaderId] FOREIGN KEY ([ASNHeaderId]) REFERENCES [ASNHeader]([ASNHeaderId]), 
    CONSTRAINT [FK_ASNShippingInfo_Carriers] FOREIGN KEY ([CarrierId]) REFERENCES [Carriers]([CarrierId]), 
    CONSTRAINT [FK_ASNShippingInfo_ShipMethod] FOREIGN KEY ([ShipMethodId]) REFERENCES [CarrierShipMethods]([ShipMethodId])

)


