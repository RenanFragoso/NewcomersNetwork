CREATE TABLE [dbo].[Choices_ImmigrantCategory] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    [Status]      CHAR (1)      NOT NULL,
    CONSTRAINT [PrimaryKey_78272d8a-189b-41de-b692-b270c9ad17fe] PRIMARY KEY CLUSTERED ([ID] ASC)
);

