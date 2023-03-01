CREATE TABLE [dbo].[LocAisle] (
    [AisleId]   INT          IDENTITY (1, 1) NOT NULL,
    [AisleName] VARCHAR (50) NOT NULL,
    [AisleSeq]  INT          NOT NULL,
    [PickZone]  INT          NOT NULL,
    [SpeedZone] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([AisleId] ASC)
);

