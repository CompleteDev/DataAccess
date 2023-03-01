CREATE TABLE [dbo].[ASNArrival]
(
	[ASNArrivalId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ASNHeaderId] INT NOT NULL, 
    [EstDate] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_ASNArrival_ASNHeder] FOREIGN KEY ([ASNHeaderId]) REFERENCES [ASNHeader]([ASNHeaderId])
)

