CREATE TABLE [dbo].[TodoItems] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Name]     NVARCHAR (MAX)   NOT NULL,
    [DueDate]  DATETIME2 (7)    NOT NULL,
    [Priority] INT              NOT NULL,
    [Status]   INT              NOT NULL,
    [City]     NVARCHAR (MAX)   NOT NULL,
    [AssignTo] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_TodoItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);