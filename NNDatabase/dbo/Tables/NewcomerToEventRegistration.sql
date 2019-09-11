CREATE TABLE [dbo].[NewcomerToEventRegistration] (
    [ID]                   INT      IDENTITY (1, 1) NOT NULL,
    [EventID]              INT      NOT NULL,
    [UserID]               INT      NOT NULL,
    [RegistrationChoiceID] INT      NOT NULL,
    [DateCreated]          DATETIME NOT NULL,
    [LastModified]         DATETIME NOT NULL,
    [IsAttending]          BIT      NOT NULL,
    [Status]               CHAR (1) NOT NULL,
    CONSTRAINT [PrimaryKey_e8041b35-936a-4614-81b8-e20162e547d5] PRIMARY KEY CLUSTERED ([ID] ASC)
);

