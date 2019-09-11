CREATE PROCEDURE [dbo].[sp_Events_GetToPublishDate]
@dateToFind datetime
AS
SELECT [EventID], [EventName], [EventDescription], [EventStartDate], [EventEndDate], [EventPublished], [EventStartPublishDate], [EventEndPublishDate], [EventFinished], [EventMaxSlots], [EventCurSlots], [EventImage], [EventCreatedBy], [EventType] 
FROM [dbo].[Events]
WHERE [EventStartPublishDate] <= @dateToFind
AND [EventEndPublishDate] >= @dateToFind