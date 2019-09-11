CREATE PROCEDURE [dbo].[sp_Events_GetRegistrations]
@cEventId nvarchar(128)

AS

SELECT [EventId], [UserId], [Email], [Name], [RegisterDate], [SlotsRequested] 
FROM [dbo].[EventRegistration]
WHERE [EventId] = @cEventId
