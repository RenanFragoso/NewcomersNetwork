CREATE TABLE [dbo].[VolunteersToNewcomers] (
    [ID]               INT      IDENTITY (1, 1) NOT NULL,
    [UserID_Volunteer] INT      NOT NULL,
    [UserID_Newcomer]  INT      NOT NULL,
    [Status]           CHAR (1) NOT NULL,
    [LastModified]     DATETIME NOT NULL,
    [State]            CHAR (1) CONSTRAINT [ColumnDefault_5f03ab93-fe29-4747-b82a-487ac22ad8ec] DEFAULT ('N') NOT NULL,
    [DateInProgress]   DATE     NULL,
    [DateCompleted]    DATE     NULL,
    CONSTRAINT [PrimaryKey_1604cfdd-6f1e-47fd-b62c-803891b87fb0] PRIMARY KEY CLUSTERED ([ID] ASC)
);

