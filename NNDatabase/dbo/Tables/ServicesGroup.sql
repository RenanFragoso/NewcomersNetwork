CREATE TABLE [dbo].[ServicesGroup] (
    [GroupId]          NVARCHAR (128) CONSTRAINT [DF_ServicesGroup_Id] DEFAULT (newid()) NOT NULL,
    [GroupName]        NVARCHAR (100) NOT NULL,
    [GroupDescription] NVARCHAR (256) NULL,
    [GroupColor]       NVARCHAR (7)   DEFAULT ('#ffffff') NOT NULL,
    [GroupIcon]        NVARCHAR (256) NULL,
    [GroupStatus]      CHAR (1)       DEFAULT ('O') NOT NULL,
    [CreateDate]       DATETIME       NOT NULL,
    [AlterDate]        DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([GroupId] ASC)
);

