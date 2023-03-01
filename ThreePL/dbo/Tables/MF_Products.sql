CREATE TABLE [dbo].[MF_Products] (
    [ProductId]  INT           IDENTITY (1, 1) NOT NULL,
    [bookID]     INT           NOT NULL,
    [isbn]       VARCHAR (MAX) NOT NULL,
    [ProdTypeId] INT           NOT NULL,
    CONSTRAINT [PK_MF_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

