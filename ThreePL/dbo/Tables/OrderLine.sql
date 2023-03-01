CREATE TABLE [dbo].[OrderLine] (
    [OrderLineId]       INT           IDENTITY (1, 1) NOT NULL,
    [StatusId]          INT           NOT NULL,
    [OrderHeaderId]     INT           NOT NULL,
    [ProductId]         INT           NOT NULL,
    [RequestedQuantity] INT           NOT NULL,
    [ReceivedDate]       DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderLineId] ASC),
    CONSTRAINT [FK_OrderLine_OrderHeaderID] FOREIGN KEY ([OrderHeaderId]) REFERENCES [dbo].[OrderHeader] ([HeaderId]),
    CONSTRAINT [FK_OrderLine_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]),
    CONSTRAINT [FK_OrderLine_StatusID] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[ItemStatus] ([StatusId])
);



GO

CREATE INDEX [IX_OrderLine_OrderHeaderID] ON [dbo].[OrderLine] ([OrderHeaderId])

GO

CREATE INDEX [IX_OrderLine_ProductID] ON [dbo].[OrderLine] ([ProductId])
