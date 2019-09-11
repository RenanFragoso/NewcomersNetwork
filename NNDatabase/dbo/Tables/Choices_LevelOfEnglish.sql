CREATE TABLE [dbo].[Choices_LevelOfEnglish] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (15) NOT NULL,
    [Status]      CHAR (1)      NOT NULL,
    CONSTRAINT [PrimaryKey_2351e906-cffc-4ef6-8d6f-a5498af8b6e2] PRIMARY KEY CLUSTERED ([ID] ASC)
);

