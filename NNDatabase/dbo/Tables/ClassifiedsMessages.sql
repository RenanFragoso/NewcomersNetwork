CREATE TABLE [dbo].[ClassifiedsMessages] (
    [ClassifiedId] NVARCHAR (128) NOT NULL,
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [From]         NVARCHAR (128) NOT NULL,
    [To]           NVARCHAR (128) NOT NULL,
    [Date]         DATETIME       NOT NULL,
    [Message]      TEXT           NOT NULL,
    [Status]       NCHAR (1)      DEFAULT ('O') NOT NULL,
    [AlterDate]    DATETIME       NOT NULL,
    [ReplyTo]      INT            DEFAULT ((0)) NULL,
    CONSTRAINT [PK_ClassifiedsMessages] PRIMARY KEY CLUSTERED ([ClassifiedId] ASC, [Id] ASC)
);

