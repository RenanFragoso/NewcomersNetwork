CREATE TABLE [dbo].[UserToNeeds] (
    [ID]           INT      IDENTITY (1, 1) NOT NULL,
    [UserID]       INT      NOT NULL,
    [NeedID]       INT      NOT NULL,
    [DateCreated]  DATETIME NOT NULL,
    [LastModified] DATETIME NOT NULL,
    CONSTRAINT [PrimaryKey_123c0a04-4d1e-4dd9-a659-f5838303800a] PRIMARY KEY CLUSTERED ([ID] ASC)
);

