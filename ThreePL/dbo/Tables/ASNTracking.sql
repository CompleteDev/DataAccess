CREATE TABLE [dbo].[ASNTracking]
(
	[ASNTrackingId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ASNHeaderId] INT NOT NULL, 
    [TrackingNumber] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_ASNTracking_ASNHeader] FOREIGN KEY ([ASNHeaderId]) REFERENCES [ASNHeader]([ASNHeaderId])

)

