CREATE TABLE [dbo].[ClassifiedsGroups] (
    [Id]               NVARCHAR (128) DEFAULT (newid()) NOT NULL,
    [GroupName]        NVARCHAR (100) NOT NULL,
    [GroupDescription] NVARCHAR (256) NULL,
    [GroupColor]       NVARCHAR (7)   DEFAULT ('#ffffff') NOT NULL,
    [GroupIcon]        NVARCHAR (256) NULL,
    [GroupStatus]      CHAR (1)       DEFAULT ('O') NOT NULL,
    [CreateDate]       DATETIME       NOT NULL,
    [AlterDate]        DATETIME       NOT NULL,
    [TextColor] NVARCHAR(7) NOT NULL DEFAULT ('#000'), 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

