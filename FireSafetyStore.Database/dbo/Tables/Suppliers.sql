CREATE TABLE [dbo].[Suppliers] (
    [SupplierId] UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (50)    NOT NULL,
    [Address]    NVARCHAR (150)   NOT NULL,
    [Phone]      NVARCHAR (50)    NOT NULL,
    [Email]      NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED ([SupplierId] ASC)
);

