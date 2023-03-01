CREATE TABLE [dbo].[MF_Prod_Definitions]
(
	[DefinitionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProdId] INT NOT NULL, 
    [ClientId] INT NOT NULL, 
    [DefinitionValue] INT NOT NULL, 
    [InductionDate] DATETIME2 NOT NULL DEFAULT (getdate()), 
    [DateInt] INT NOT NULL, 
    [YearInt] INT NOT NULL
)
