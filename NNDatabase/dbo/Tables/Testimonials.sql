CREATE TABLE [dbo].[Testimonials] (
    [Id]          NVARCHAR (128)  DEFAULT (newid()) NOT NULL,
    [Author]      NVARCHAR (128)  NOT NULL,
    [Content]     NVARCHAR (2048) NULL,
    [CreateDate]  DATETIME        NOT NULL,
    [AlterDate]   DATETIME        NOT NULL,
    [Status]      NCHAR (1)       DEFAULT ('B') NULL,
    [AuthorTitle] NVARCHAR (100)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

