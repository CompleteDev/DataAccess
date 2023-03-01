CREATE TABLE [dbo].[Products]
(
	[ProductId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientProdNumber] VARCHAR(50) NOT NULL, 
    [SKU] VARCHAR(50) NOT NULL, 
    [ProductTitle] VARCHAR(250) NOT NULL, 
    [ProductDescription] VARCHAR(MAX) NOT NULL, 
    [ProductCondition] INT NOT NULL
)
