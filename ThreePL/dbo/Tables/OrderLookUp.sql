CREATE TABLE [dbo].[OrderLookUp]
(
	[LookUpId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [NewHeaderId] INT NOT NULL, 
    [OldHeaderId] INT NOT NULL, 
    [OrderNumber] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_OrderLookUp_OrderHeader] FOREIGN KEY ([NewHeaderId]) REFERENCES [OrderHeader]([HeaderId])
)

GO

CREATE INDEX [IX_OrderLookUp_NewHeaderID] ON [dbo].[OrderLookUp] ([NewHeaderId])

GO

CREATE INDEX [IX_OrderLookUp_OldHeaderID] ON [dbo].[OrderLookUp] ([OldHeaderId])
