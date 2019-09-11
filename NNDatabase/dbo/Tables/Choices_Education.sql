CREATE TABLE [dbo].[Choices_Education] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    [Status]      CHAR (1)      NOT NULL,
    CONSTRAINT [PrimaryKey_6e668e33-5657-4a3d-ab54-1f11015ab0ae] PRIMARY KEY CLUSTERED ([ID] ASC)
);

