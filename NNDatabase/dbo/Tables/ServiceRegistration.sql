CREATE TABLE [dbo].[ServiceRegistration] (
    [ServiceId]      NVARCHAR (128) NOT NULL,
    [ScheduleId]     NVARCHAR (128) NOT NULL,
    [ScheduleItem]   INT            NOT NULL,
    [RegisterId]     INT            IDENTITY (1, 1) NOT NULL,
    [UserId]         NVARCHAR (128) NULL,
    [Email]          NVARCHAR (128) NULL,
    [Name]           NVARCHAR (100) NULL,
    [RegisterDate]   DATETIME       NOT NULL,
    [SlotsRequested] INT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([RegisterId] ASC, [ServiceId] ASC, [ScheduleId] ASC, [ScheduleItem] ASC)
);

