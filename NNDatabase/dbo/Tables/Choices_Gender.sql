CREATE TABLE [dbo].[Choices_Gender] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (10) NOT NULL,
    [Status]      CHAR (1)      NOT NULL,
    CONSTRAINT [PrimaryKey_ca82ce0e-815c-4765-aa67-a4184552af36] PRIMARY KEY CLUSTERED ([ID] ASC)
);

