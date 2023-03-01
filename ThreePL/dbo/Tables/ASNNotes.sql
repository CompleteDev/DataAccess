CREATE TABLE [dbo].[ASNNotes]
(
	[ASNNoteId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ASNHeaderId] INT NOT NULL, 
    [Note] TEXT NOT NULL, 
    CONSTRAINT [FK_ASNNotes_ASNHeader] FOREIGN KEY ([ASNHeaderId]) REFERENCES [ASNHeader]([ASNHeaderId])

)

