CREATE TABLE [dbo].[OrderActionItems] (
    [ActionId]      INT           IDENTITY (1, 1) NOT NULL,
    [OrderHeaderId] INT           NOT NULL,
    [ClientId]      INT           NOT NULL,
    [ActionMessage] VARCHAR (MAX) NOT NULL,
    [Viewed]        INT           DEFAULT ((0)) NOT NULL,
    [Resolved]      INT           DEFAULT ((0)) NOT NULL,
    [ActionDate]    DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ActionId] ASC),
    CONSTRAINT [FK_OrderActionItems_ToClient] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]),
    CONSTRAINT [FK_OrderActionItems_ToOrderHeader] FOREIGN KEY ([OrderHeaderId]) REFERENCES [dbo].[OrderHeader] ([HeaderId])
);

