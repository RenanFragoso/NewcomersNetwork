CREATE TABLE [dbo].[Choices_Ministry] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    [Status]      CHAR (1)      NOT NULL,
    CONSTRAINT [PrimaryKey_4178c1b6-47ee-4668-881f-6eca10ee4064] PRIMARY KEY CLUSTERED ([ID] ASC)
);

