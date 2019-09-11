CREATE TABLE [dbo].[User] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (100) NOT NULL,
    [Email]        NVARCHAR (100) NOT NULL,
    [DateCreated]  DATETIME       NOT NULL,
    [Status]       CHAR (1)       NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    CONSTRAINT [PrimaryKey_508f15f7-b21e-4ce2-aa8f-29d7f6d80f50] PRIMARY KEY CLUSTERED ([ID] ASC)
);

