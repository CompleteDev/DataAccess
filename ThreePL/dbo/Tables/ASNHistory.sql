CREATE TABLE [dbo].[ASNHistory]
(
	[ASNHistoryId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ASNHeaderId] INT NOT NULL, 
    [ASNMessage] VARCHAR(MAX) NOT NULL, 
    [ASNStatusId] INT NOT NULL, 
    CONSTRAINT [FK_ASNHistory_ASNHeader] FOREIGN KEY ([ASNHeaderId]) REFERENCES [ASNHeader]([ASNHeaderId]), 
    CONSTRAINT [FK_ASNHistory_ItemStatus] FOREIGN KEY ([ASNStatusId]) REFERENCES [dbo].[ItemStatus] ([StatusId])
)
