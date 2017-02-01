CREATE PROCEDURE [dbo].[sp_Schedule_Get]
@cScheduleId nvarchar(128),
@cServiceId nvarchar(128)

AS

SELECT *
FROM [dbo].[ServicesSchedule]
WHERE [Id] = @cScheduleId
AND [ServiceId] = @cServiceId