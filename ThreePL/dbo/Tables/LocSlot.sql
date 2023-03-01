CREATE TABLE [dbo].[LocSlot] (
    [SlotId]    INT          IDENTITY (1, 1) NOT NULL,
    [ShelfID]   INT          NOT NULL,
    [SlotName]  VARCHAR (50) NOT NULL,
    [SlotSeq]   INT          NOT NULL,
    [PickZone]  INT          NOT NULL,
    [SpeedZone] NCHAR (10)   NOT NULL,
    [Filled]    INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([SlotId] ASC)
);

