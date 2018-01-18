CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailsId] UNIQUEIDENTIFIER NOT NULL,
    [OrderId]        UNIQUEIDENTIFIER NOT NULL,
    [ItemId]         UNIQUEIDENTIFIER NOT NULL,
    [Quantity]       INT              NOT NULL,
    [Rate]           MONEY            NOT NULL,
    [Total]          MONEY            NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([OrderDetailsId] ASC),
    CONSTRAINT [FK_OrderDetails_OrderMaster] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[OrderMaster] ([OrderId]),
    CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Products] ([ItemId])
);

