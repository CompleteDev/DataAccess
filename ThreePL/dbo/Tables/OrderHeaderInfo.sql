CREATE TABLE [dbo].[OrderHeaderInfo] (
    [HeaderInfoId]       INT          IDENTITY (1, 1) NOT NULL,
    [OrderHeaderId]      INT          NOT NULL,
    [CarrierId]          INT          NOT NULL,
    [ShipMethodId]       INT          NOT NULL,
    [MustShipDate]       DATETIME     NOT NULL,
    [PONumber]           VARCHAR (50) NULL,
    [ShipmentRetryCount] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([HeaderInfoId] ASC),
    CONSTRAINT [FK_OrderHeaderInfo_CarrierID] FOREIGN KEY ([CarrierId]) REFERENCES [dbo].[Carriers] ([CarrierId]),
    CONSTRAINT [FK_OrderHeaderInfo_OrderHeader] FOREIGN KEY ([OrderHeaderId]) REFERENCES [dbo].[OrderHeader] ([HeaderId]),
    CONSTRAINT [FK_OrderHeaderInfo_ShipMethodId] FOREIGN KEY ([ShipMethodId]) REFERENCES [dbo].[CarrierShipMethods] ([ShipMethodId])
);


