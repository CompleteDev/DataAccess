CREATE TABLE [dbo].[OrderLineHistory] (
    [HistoryID]     INT           IDENTITY (1, 1) NOT NULL,
    [OrderHeaderId] INT           NOT NULL,
    [OrderLineId]   INT           NOT NULL,
    [OrderStatusId] INT           NOT NULL,
    [Info]          VARCHAR (MAX) NOT NULL,
    [UserName] VARCHAR(MAX) NOT NULL DEFAULT 'System',
    [ShippedQTY]    INT           DEFAULT 0 NOT NULL,
    [HistoryDate]   DATETIME2 (7) DEFAULT (getdate()) NOT NULL, 
    PRIMARY KEY CLUSTERED ([HistoryID] ASC),
    CONSTRAINT [FK_OrderLineHistory_OrderHeaderID] FOREIGN KEY ([OrderHeaderId]) REFERENCES [dbo].[OrderHeader] ([HeaderId]),
    CONSTRAINT [FK_OrderLineHistory_OrderLineID] FOREIGN KEY ([OrderLineId]) REFERENCES [dbo].[OrderLine] ([OrderLineId]),
    CONSTRAINT [FK_OrderLineHistory_StatusID] FOREIGN KEY ([OrderStatusId]) REFERENCES [dbo].[ItemStatus] ([StatusId])
);



GO

CREATE INDEX [IX_OrderLineHistory_OrderHeaderID] ON [dbo].[OrderLineHistory] ([OrderHeaderId])

GO

CREATE INDEX [IX_OrderLineHistory_OrderLineID] ON [dbo].[OrderLineHistory] ([OrderLineId])
