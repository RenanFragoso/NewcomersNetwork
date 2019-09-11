CREATE TABLE [dbo].[Events] (
    [Id]               NVARCHAR (128)  CONSTRAINT [DF_Events_Id] DEFAULT (newid()) NOT NULL,
    [Name]             NVARCHAR (50)   NOT NULL,
    [Description]      NVARCHAR (1000) NOT NULL,
    [StartDate]        DATETIME        NOT NULL,
    [EndDate]          DATETIME        NOT NULL,
    [Published]        BIT             CONSTRAINT [DF_Events_Published] DEFAULT ((0)) NOT NULL,
    [StartPublishDate] DATETIME        NOT NULL,
    [EndPublishDate]   DATETIME        NOT NULL,
    [Finished]         BIT             CONSTRAINT [DF_Events_Finished] DEFAULT ((0)) NULL,
    [MaxSlots]         INT             NULL,
    [CurSlots]         INT             NULL,
    [Image]            NVARCHAR (255)  NULL,
    [CreatedBy]        NVARCHAR (128)  NOT NULL,
    [Type]             INT             NOT NULL,
    [ExternalLink]     NVARCHAR (256)  NULL,
    [Title]            NVARCHAR (50)   NULL,
    [SubTitle]         NVARCHAR (100)  NULL,
    [Text1]            NTEXT           NULL,
    [Text2]            NTEXT           NULL,
    [Footer]           NTEXT           NULL,
    [HeadImg]          NVARCHAR (256)  NULL,
    [Location]         NTEXT           NULL,
    [CreateDate]       DATETIME        NOT NULL,
    [AlterDate]        DATETIME        NOT NULL,
    [Status]           NCHAR (1)       DEFAULT ('O') NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Created by user.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Current slots taken.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'CurSlots';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Description of the Event', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'End Date', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date to stop publishing the event on the start page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'EndPublishDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Event Finished (or full)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'Finished';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Address to the image to publish.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'Image';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maximum slots permited by the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'MaxSlots';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the Event', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Publish event on the start page?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'Published';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Start Date', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date to start publishing the event on the start page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'StartPublishDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Event Type.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Events', @level2type = N'COLUMN', @level2name = N'Type';

