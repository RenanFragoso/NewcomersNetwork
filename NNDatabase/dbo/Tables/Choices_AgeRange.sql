CREATE TABLE [dbo].[Choices_AgeRange] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (25) NOT NULL,
    [Status]      CHAR (1)      NOT NULL,
    CONSTRAINT [PrimaryKey_ac061c8c-4e92-421a-b35d-7acc2ce34b43] PRIMARY KEY CLUSTERED ([ID] ASC)
);

