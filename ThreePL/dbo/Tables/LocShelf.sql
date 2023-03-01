CREATE TABLE [dbo].[LocShelf] (
    [ShelfId]   INT          IDENTITY (1, 1) NOT NULL,
    [BayID]     INT          NOT NULL,
    [ShelfName] VARCHAR (50) NOT NULL,
    [ShelfSeq]  INT          NOT NULL,
    [PickZone]  INT          NOT NULL,
    [SpeedZone] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([ShelfId] ASC)
);

