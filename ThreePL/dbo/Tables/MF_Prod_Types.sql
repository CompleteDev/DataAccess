CREATE TABLE [dbo].[MF_Prod_Types] (
    [Prod_Type_Id] INT          IDENTITY (1, 1) NOT NULL,
    [Prod_Type]    VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_MF_Prod_Types] PRIMARY KEY CLUSTERED ([Prod_Type_Id] ASC)
);

