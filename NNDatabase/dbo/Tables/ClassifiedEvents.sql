CREATE TABLE [dbo].[ClassifiedEvents] (
    [ClassifiedId] NVARCHAR (128) NOT NULL,
    [EventType]    NCHAR (1)      NOT NULL,
    [CreateDate]   DATETIME       NOT NULL,
    [From]         NVARCHAR (128) NOT NULL,
    [To]           NVARCHAR (128) NULL,
    [Contents]     NTEXT          NULL,
    PRIMARY KEY CLUSTERED ([CreateDate] ASC, [EventType] ASC, [ClassifiedId] ASC)
);

