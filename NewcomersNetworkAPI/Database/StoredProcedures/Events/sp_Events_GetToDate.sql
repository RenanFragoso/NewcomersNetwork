CREATE PROCEDURE [dbo].[sp_Events_GetToDate]
@dateToFind datetime
AS
SELECT [EventID], [EventName], [EventDescription], [EventStartDate], [EventEndDate], [EventPublished], [EventStartPublishDate], [EventEndPublishDate], [EventFinished], [EventMaxSlots], [EventCurSlots], [EventImage], [EventCreatedBy], [EventType] 
FROM [dbo].[Events]
WHERE [EventStartDate] <= @dateToFind
AND [EventEndDate] >= @dateToFind