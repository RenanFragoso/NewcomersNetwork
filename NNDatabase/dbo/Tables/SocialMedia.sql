CREATE TABLE [dbo].[SocialMedia] (
    [Id]          NVARCHAR (128) DEFAULT (newid()) NOT NULL,
    [Media]       NVARCHAR (50)  NOT NULL,
    [AccessToken] NVARCHAR (256) NULL,
    [LastRead]    DATETIME       NULL,
    [Content]     TEXT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

