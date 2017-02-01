CREATE PROCEDURE [dbo].[sp_ScheduleItem_GetAll]
@cScheduleId nvarchar(128)

AS

SELECT *
FROM [dbo].[ServiceScheduleItem]
WHERE [ScheduleId] = @cScheduleId