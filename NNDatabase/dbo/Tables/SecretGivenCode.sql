CREATE TABLE [dbo].[SecretGivenCode] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [GivenCode] NVARCHAR (20) NOT NULL,
    [Status]    CHAR (1)      NOT NULL,
    CONSTRAINT [PrimaryKey_a2797a71-224a-4257-8d4a-6c91eb26e2a8] PRIMARY KEY CLUSTERED ([ID] ASC)
);

