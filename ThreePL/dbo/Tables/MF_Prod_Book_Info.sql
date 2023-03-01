CREATE TABLE [dbo].[MF_Prod_Book_Info] (
    [Prod_Info_Id] INT           IDENTITY (1, 1) NOT NULL,
    [Prod_Id]      INT           NOT NULL,
    [Author]       VARCHAR (50)  NOT NULL,
    [Title]        VARCHAR (MAX) NOT NULL,
    [Publisher]    VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_MF_Prod_Book_Info] PRIMARY KEY CLUSTERED ([Prod_Info_Id] ASC)
);

