CREATE PROCEDURE [dbo].[sp_Schedule_Delete]
@cServiceId nvarchar(128),
@cScheduleId nvarchar(128)

AS
BEGIN 
	DELETE FROM [dbo].[ServiceScheduleItem]
	WHERE [ScheduleId] = @cScheduleId
END

BEGIN
	DELETE FROM [dbo].[ServicesSchedule]
	WHERE [ServiceId] = @cServiceId
	AND [Id] = @cScheduleId
END
 