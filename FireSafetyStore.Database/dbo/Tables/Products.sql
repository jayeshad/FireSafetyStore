CREATE TABLE [dbo].[Products] (
    [ItemId]           UNIQUEIDENTIFIER NOT NULL,
    [ItemName]         NVARCHAR (50)    NOT NULL,
    [Description]      NVARCHAR (250)   NOT NULL,
    [BrandId]          UNIQUEIDENTIFIER NOT NULL,
    [CategoryId]       UNIQUEIDENTIFIER NOT NULL,
    [Quantity]         INT              NOT NULL,
    [Rate]             DECIMAL (18, 2)  NOT NULL,
    [Image]            VARBINARY (MAX)  NOT NULL,
    [ImagePath]        NVARCHAR (2500)  NOT NULL,
    [OriginalFileName] NVARCHAR (1000)  NOT NULL,
    [UpdatedAt]        DATETIME         NOT NULL,
    [IsActive]         BIT              NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ItemId] ASC),
    CONSTRAINT [FK_Products_Brands] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brands] ([BrandId]),
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId])
);

