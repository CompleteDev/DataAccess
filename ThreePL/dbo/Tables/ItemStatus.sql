CREATE TABLE [dbo].[ItemStatus]
(
	[StatusId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemStatus] VARCHAR(50) NOT NULL, 
    [SortOrder] INT NOT NULL
)
