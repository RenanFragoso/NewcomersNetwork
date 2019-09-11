CREATE TABLE [dbo].[Choices_NeedsCategory] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (100) NOT NULL,
    [Status]      CHAR (1)       NOT NULL,
    [AreaCD]      CHAR (3)       NOT NULL,
    CONSTRAINT [PrimaryKey_b3d85178-45b9-494f-9c39-bf8c18c6e1d6] PRIMARY KEY CLUSTERED ([ID] ASC)
);

