CREATE PROCEDURE [dbo].[sp_ScheduleItem_Delete]
@nId int,
@cScheduleId nvarchar(128)

AS

DELETE FROM [dbo].[ServiceScheduleItem]
WHERE [ScheduleId] = @cScheduleId
AND [Id] = @nId
 