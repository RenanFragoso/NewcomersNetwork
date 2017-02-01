CREATE PROCEDURE [dbo].[sp_EventDetail_GetByEvntID]
@cEventId nvarchar(128)
AS
SELECT [Id], [EventId], [Title], [SubTitle], [Text1], [Text2], [Footer], [HeadImg], [Location]
FROM [dbo].[EventDetail]
WHERE [EventId] = @cEventId