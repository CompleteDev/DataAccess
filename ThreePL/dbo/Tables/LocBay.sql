CREATE TABLE [dbo].[LocBay] (
    [BayId]     INT          IDENTITY (1, 1) NOT NULL,
    [AisleID]   INT          NOT NULL,
    [BayName]   VARCHAR (50) NOT NULL,
    [BaySeq]    INT          NOT NULL,
    [PickZone]  INT          NOT NULL,
    [SpeedZone] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([BayId] ASC)
);

