CREATE TABLE [dbo].[PaymentTypes] (
    [PaymentTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [PaymentType]   VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([PaymentTypeId] ASC)
);

