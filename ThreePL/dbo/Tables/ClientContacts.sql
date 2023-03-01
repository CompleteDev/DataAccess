CREATE TABLE [dbo].[ClientContacts]
(
	[ContactId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientId] INT NOT NULL,
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [IsActive] INT NOT NULL DEFAULT 1, 
    [IsPrimary] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_ClientContacts_Clients] FOREIGN KEY ([ClientId]) REFERENCES [Clients]([ClientId])
)

GO

CREATE INDEX [IX_ClientContacts_ClientId] ON [dbo].[ClientContacts] ([ClientId])
