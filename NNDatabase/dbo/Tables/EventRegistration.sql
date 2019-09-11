CREATE TABLE [dbo].[EventRegistration] (
    [EventID]        NVARCHAR (128) NOT NULL,
    [UserID]         NVARCHAR (128) NULL,
    [Email]          NVARCHAR (256) NULL,
    [Name]           NVARCHAR (100) NULL,
    [RegisterDate]   DATETIME       NOT NULL,
    [SlotsRequested] INT            CONSTRAINT [DF_EventRegistration_EventRegistrationQtdPers] DEFAULT ((1)) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registration by event', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRegistration';

