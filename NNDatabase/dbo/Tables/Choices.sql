CREATE TABLE [dbo].[Choices] (
    [ChoiceName]  NVARCHAR (256) NOT NULL,
    [ChoiceGroup] NVARCHAR (256) NULL,
    [ChoiceSP]    NVARCHAR (256) NOT NULL,
    [Status]      NCHAR (1)      DEFAULT ('O') NULL,
    PRIMARY KEY CLUSTERED ([ChoiceName] ASC)
);

