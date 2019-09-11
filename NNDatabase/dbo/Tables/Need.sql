CREATE TABLE [dbo].[Need] (
    [ID]                INT             IDENTITY (1, 1) NOT NULL,
    [Description]       NVARCHAR (1000) NOT NULL,
    [Status]            CHAR (1)        NOT NULL,
    [State]             CHAR (1)        CONSTRAINT [ColumnDefault_71958fc9-c04e-4371-be3e-a19ac2a604a9] DEFAULT ('A') NOT NULL,
    [DateCreated]       DATETIME        NOT NULL,
    [DateMet]           DATETIME        NULL,
    [LastModified]      DATETIME        NOT NULL,
    [NeedsCategoryID]   INT             NOT NULL,
    [MetByEmail]        NVARCHAR (100)  NULL,
    [MetByName]         NVARCHAR (100)  NULL,
    [NeedsGUID]         CHAR (36)       NOT NULL,
    [NeedsAreaCD]       CHAR (3)        NOT NULL,
    [DatePending]       DATETIME        NULL,
    [ReminderEmailSent] BIT             CONSTRAINT [ColumnDefault_4932f7d8-1bc6-41c4-a6b4-4e5947baee54] DEFAULT ((0)) NULL,
    CONSTRAINT [PrimaryKey_d2e95fc7-9ad9-464e-bd09-1f736b7cbeff] PRIMARY KEY CLUSTERED ([ID] ASC)
);

