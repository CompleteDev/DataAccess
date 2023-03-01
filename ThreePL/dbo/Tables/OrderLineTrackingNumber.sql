CREATE TABLE [dbo].[OrderLineTrackingNumber] (
    [TrackingId]     INT           IDENTITY (1, 1) NOT NULL,
    [OrderLineId]    INT           NOT NULL,
    [TrackingNumber] VARCHAR (MAX) NOT NULL,
    [TrackingDate]   DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([TrackingId] ASC),
    CONSTRAINT [FK_OrderLineTrackingNumber_ToOrderLine] FOREIGN KEY ([OrderLineId]) REFERENCES [dbo].[OrderLine] ([OrderLineId])
);



GO

CREATE INDEX [IX_OrderLineTrackingNumber_OrderLineID] ON [dbo].[OrderLineTrackingNumber] ([OrderLineId])
