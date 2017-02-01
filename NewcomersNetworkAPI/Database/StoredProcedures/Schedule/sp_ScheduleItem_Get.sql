CREATE PROCEDURE [dbo].[sp_ScheduleItem_Get]
@cScheduleId nvarchar(128),
@nId int

AS

SELECT *
FROM [dbo].[ServiceScheduleItem]
WHERE [ScheduleId] = @cScheduleId
AND [Id] = @nId