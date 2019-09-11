CREATE TABLE [dbo].[SecretRegistrationCode] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [SecretCode] NVARCHAR (50) NOT NULL,
    [Status]     CHAR (1)      NOT NULL,
    CONSTRAINT [PrimaryKey_faeb1e2c-ebac-44e1-8577-30f617ee65d0] PRIMARY KEY CLUSTERED ([ID] ASC)
);

