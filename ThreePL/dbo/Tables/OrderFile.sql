CREATE TABLE [dbo].[OrderFile] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [InsertedDate]  DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [StatusId]      INT           NOT NULL,
    [OrderHeaderId] INT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


