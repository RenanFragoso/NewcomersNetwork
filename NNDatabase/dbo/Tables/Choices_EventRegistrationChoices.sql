CREATE TABLE [dbo].[Choices_EventRegistrationChoices] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (100) NOT NULL,
    [EventID]     INT            NOT NULL,
    [Status]      CHAR (1)       NOT NULL,
    CONSTRAINT [PrimaryKey_7bd8a9e4-4f17-46a2-8912-81a811fd6165] PRIMARY KEY CLUSTERED ([ID] ASC)
);

