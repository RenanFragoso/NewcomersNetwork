CREATE TABLE [dbo].[Classifieds] (
    [Id]         NVARCHAR (128) DEFAULT (newid()) NOT NULL,
    [Title]      NVARCHAR (100) NOT NULL,
    [Text]       NTEXT          NOT NULL,
    [CreatedBy]  NVARCHAR (128) NOT NULL,
    [Category]   NVARCHAR (128) NOT NULL,
    [Type]       NCHAR (1)      NOT NULL,
    [Image]      NVARCHAR (256) NULL,
    [CreateDate] DATETIME       NOT NULL,
    [AlterDate]  DATETIME       NOT NULL,
    [Status]     NCHAR (1)      DEFAULT ('P') NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

