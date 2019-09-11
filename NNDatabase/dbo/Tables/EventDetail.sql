CREATE TABLE [dbo].[EventDetail] (
    [EventId]  NVARCHAR (128) NOT NULL,
    [Title]    NVARCHAR (50)  NULL,
    [SubTitle] NVARCHAR (100) NULL,
    [Text1]    NTEXT          NULL,
    [Text2]    NTEXT          NULL,
    [Footer]   NTEXT          NULL,
    [HeadImg]  NVARCHAR (256) NULL,
    [Location] NTEXT          NULL,
    CONSTRAINT [PK_EventDetail] PRIMARY KEY CLUSTERED ([EventId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [EventID]
    ON [dbo].[EventDetail]([EventId] ASC);

