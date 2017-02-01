CREATE PROCEDURE [dbo].[sp_Schedule_GetAll]
@cServiceId nvarchar(128)

AS

SELECT *
FROM [dbo].[ServicesSchedule]
WHERE [ServiceId] = @cServiceId