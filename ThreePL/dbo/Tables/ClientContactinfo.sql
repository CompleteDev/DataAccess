CREATE TABLE [dbo].[ClientContactInfo]
(
	[ContactInfoId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ClientContactId] INT NOT NULL, 
    [ContactTypeID] INT NOT NULL, 
    [ContactInfo] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_ClientContactInfo_ClientContact] FOREIGN KEY ([ClientContactId]) REFERENCES [ClientContacts]([ContactId]), 
    CONSTRAINT [FK_ClientContactInfo_ContactTypeId] FOREIGN KEY ([ContactTypeID]) REFERENCES [ContactType]([ContactTypeId])
)

GO

CREATE INDEX [IX_ClientContactInfo_ClientId] ON [dbo].[ClientContactInfo] ([ClientContactId])
