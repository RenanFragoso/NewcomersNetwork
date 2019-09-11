CREATE TABLE [dbo].[ServicesSchedule] (
    [Id]                NVARCHAR (128) DEFAULT (newid()) NOT NULL,
    [ServiceId]         NVARCHAR (128) NOT NULL,
    [Name]              NVARCHAR (50)  NOT NULL,
    [StartDate]         DATETIME       NOT NULL,
    [EndDate]           DATETIME       NOT NULL,
    [Responsible]       NVARCHAR (128) NULL,
    [DoW]               VARCHAR (7)    NULL,
    [Enable]            BIT            CONSTRAINT [DF_ServicesSchedule_ServiceScheduleEnable] DEFAULT ((1)) NOT NULL,
    [Description]       NVARCHAR (100) NULL,
    [Slots]             INT            CONSTRAINT [DF_ServicesSchedule_ServiceScheduleSlotsPerDay] DEFAULT ((0)) NULL,
    [CreateDate]        DATETIME       NOT NULL,
    [AlterDate]         DATETIME       NOT NULL,
    [UniqueInscription] BIT            DEFAULT ((1)) NOT NULL,
    [RequireRegister]   BIT            DEFAULT ((0)) NOT NULL,
    [Status]            NCHAR (1)      DEFAULT ('O') NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Schedule for Services', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ServicesSchedule';

