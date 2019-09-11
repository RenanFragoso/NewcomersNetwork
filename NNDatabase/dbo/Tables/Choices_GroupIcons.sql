CREATE TABLE [dbo].[Choices_GroupIcons] (
    [id]     NVARCHAR (25) NOT NULL,
    [text]   NVARCHAR (50) NULL,
    [status] NCHAR (1)     DEFAULT ('O') NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

