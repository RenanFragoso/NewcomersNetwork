CREATE PROCEDURE [dbo].[sp_Calendar_GetEvents]
@dStartDate datetime,
@dEndDate datetime

AS

SELECT [EventID], [EventName], [EventDescription], [EventStartDate], [EventEndDate], [EventPublished], [EventStartPublishDate], [EventEndPublishDate], [EventFinished], [EventMaxSlots], [EventCurSlots], [EventImage], [EventCreatedBy], [EventType] 
FROM [dbo].[Events]
WHERE [EventStartDate] >= @dStartDate
AND [EventEndDate] <= @dEndDate