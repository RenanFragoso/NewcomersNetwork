CREATE TABLE [dbo].[Settings_Banners] (
    [Id]              NVARCHAR (128) DEFAULT (newid()) NOT NULL,
    [Link]            NVARCHAR (256) NULL,
    [Title1]          NVARCHAR (100) NULL,
    [Title2]          NVARCHAR (100) NULL,
    [Status]          NCHAR (1)      DEFAULT ('O') NOT NULL,
    [StartPublishing] DATETIME       NULL,
    [EndPublishing]   DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

