CREATE TABLE [dbo].[Brands] (
    [BrandId]     UNIQUEIDENTIFIER NOT NULL,
    [Description] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED ([BrandId] ASC)
);

