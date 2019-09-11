CREATE TABLE [dbo].[Choices_Occupation] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (255) NOT NULL,
    [Status]      CHAR (1)       NOT NULL,
    CONSTRAINT [PrimaryKey_c0573602-c9f0-463e-b7df-bc4cebf6d0a4] PRIMARY KEY CLUSTERED ([ID] ASC)
);

