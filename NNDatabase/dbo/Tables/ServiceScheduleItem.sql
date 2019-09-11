CREATE TABLE [dbo].[ServiceScheduleItem] (
    [ScheduleId]        NVARCHAR (128) NOT NULL,
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [StartDate]         DATETIME       NOT NULL,
    [EndDate]           DATETIME       NOT NULL,
    [Slots]             INT            DEFAULT ((1)) NOT NULL,
    [UniqueInscription] BIT            DEFAULT ((1)) NULL,
    [RequireRegister]   BIT            DEFAULT ((0)) NULL,
    [CreateDate]        DATETIME       NOT NULL,
    [AlterDate]         DATETIME       NOT NULL,
    [MaxSlots]          INT            DEFAULT ((1)) NOT NULL,
    [Status]            NCHAR (1)      DEFAULT ('O') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC, [ScheduleId] ASC)
);

