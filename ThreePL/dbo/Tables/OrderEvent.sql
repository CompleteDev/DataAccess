CREATE TABLE [dbo].[OrderEvent] (
    [OrderEventId]  INT           IDENTITY (1, 1) NOT NULL,
    [OrderHeaderId] INT           NOT NULL,
    [OrderStatusId] INT           NOT NULL,
    [OrderInfo]     VARCHAR (MAX) NOT NULL,
    [UserName] VARCHAR(MAX) NOT NULL DEFAULT 'System',
    [EventDate]     DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderEventId] ASC),
    CONSTRAINT [FK_OrderEvent_OrderHeaderId] FOREIGN KEY ([OrderHeaderId]) REFERENCES [dbo].[OrderHeader] ([HeaderId]),
    CONSTRAINT [FK_OrderEvent_StatusID] FOREIGN KEY ([OrderStatusId]) REFERENCES [dbo].[ItemStatus] ([StatusId])
);



GO

CREATE INDEX [IX_OrderEvent_OrderHeaderID] ON [dbo].[OrderEvent] ([OrderHeaderId])
