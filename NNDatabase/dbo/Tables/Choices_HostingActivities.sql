CREATE TABLE [dbo].[Choices_HostingActivities] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (300) NOT NULL,
    [Status]      CHAR (1)       CONSTRAINT [ColumnDefault_58647c03-af7a-4cd6-91e5-3e4825832ff0] DEFAULT ('A') NOT NULL,
    CONSTRAINT [PrimaryKey_773ce14d-e5be-4afd-9ea7-00264cff59dc] PRIMARY KEY CLUSTERED ([ID] ASC)
);

