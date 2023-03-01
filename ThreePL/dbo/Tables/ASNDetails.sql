CREATE TABLE [dbo].[ASNDetails]
(
	[ASNDetailId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ASNHeaderId] INT NOT NULL, 
    [SKU] VARCHAR(50) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Price] DECIMAL(18, 2) NOT NULL, 
    CONSTRAINT [FK_ASNDetails_ASNHeader] FOREIGN KEY ([ASNHeaderId]) REFERENCES [ASNHeader]([ASNHeaderId])

)

