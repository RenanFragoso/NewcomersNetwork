CREATE TABLE [dbo].[NewcomerToHostingActivities] (
    [ID]                INT      IDENTITY (1, 1) NOT NULL,
    [UserID]            INT      NOT NULL,
    [HostingActivityID] INT      NOT NULL,
    [IsSelected]        BIT      CONSTRAINT [ColumnDefault_c8f20d3e-7ade-445e-b895-c3c46acfd304] DEFAULT ((0)) NOT NULL,
    [DateCreated]       DATETIME NOT NULL,
    [LastModified]      DATETIME NOT NULL,
    [State]             CHAR (1) CONSTRAINT [ColumnDefault_8ba2fd5c-27e5-4515-9247-d9b025632d94] DEFAULT ('N') NOT NULL,
    [DateInProgress]    DATE     NULL,
    [DateCompleted]     DATE     NULL,
    CONSTRAINT [PrimaryKey_defb9aac-1b73-4068-9083-597ef2d2285d] PRIMARY KEY CLUSTERED ([ID] ASC)
);

