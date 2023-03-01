CREATE TABLE [dbo].[OrderHeader] (
    [HeaderId] INT           IDENTITY (1, 1) NOT NULL,
    [StatusId] INT           NOT NULL,
    [ClientId] INT           NOT NULL,
    [OrderTypeId] INT DEFAULT 0,
    [OrderNumber] VARCHAR(MAX) NULL,
    [SentDate] DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [DateInt]  INT           NOT NULL, 
    PRIMARY KEY CLUSTERED ([HeaderId] ASC),
    CONSTRAINT [FK_OrderHeader_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]),
    CONSTRAINT [FK_OrderHeader_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[ItemStatus] ([StatusId])
);



GO

CREATE INDEX [IX_OrderHeader_ClientID] ON [dbo].[OrderHeader] ([ClientId])

GO


