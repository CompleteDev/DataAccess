CREATE TABLE [dbo].[ASNShipType]
(
	[ASNShipTypeId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ASNHeaderId] INT NOT NULL, 
    [ShipTypeId] INT NOT NULL, 
    CONSTRAINT [FK_ASNShipType_HeaderId] FOREIGN KEY ([ASNHeaderId]) REFERENCES [ASNHeader]([ASNHeaderId]), 
    CONSTRAINT [FK_ASNShipType_ShipType] FOREIGN KEY ([ShipTypeId]) REFERENCES [ShipmentTypes]([ShipmentTypeId])
)
