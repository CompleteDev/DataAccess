CREATE TABLE [dbo].[ASNHeader] (
    [ASNHeaderId]     INT           IDENTITY (1, 1) NOT NULL,
    [AccountNumber]   VARCHAR (50)  NOT NULL,
    [VendorReference] VARCHAR (50)  NOT NULL,
    [SentDate]        DATETIME2 (7) NOT NULL,
    [Cartons]         INT           NOT NULL,
    [Pallets]         INT           NOT NULL,
    [StatusId]        INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ASNHeaderId] ASC),
    CONSTRAINT [FK_ASNHeader_ItemStatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[ItemStatus] ([StatusId])
);


