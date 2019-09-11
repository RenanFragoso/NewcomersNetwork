CREATE TABLE [dbo].[Choices_MaritalStatus] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (15) NOT NULL,
    [Status]      CHAR (1)      NOT NULL,
    CONSTRAINT [PrimaryKey_fde9e058-c66a-4f13-abde-72b9ccb01493] PRIMARY KEY CLUSTERED ([ID] ASC)
);

