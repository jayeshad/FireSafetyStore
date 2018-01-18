CREATE TABLE [dbo].[Categories] (
    [CategoryId]  UNIQUEIDENTIFIER NOT NULL,
    [Description] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

