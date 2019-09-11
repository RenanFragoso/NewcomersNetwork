CREATE TABLE [dbo].[SecretGivenCodeGUID] (
    [ID]            INT       IDENTITY (1, 1) NOT NULL,
    [GivenCodeGUID] CHAR (36) NOT NULL,
    [Status]        CHAR (1)  NOT NULL,
    CONSTRAINT [PrimaryKey_7110641e-af87-4cf3-8f62-cdca37ad02b1] PRIMARY KEY CLUSTERED ([ID] ASC)
);

