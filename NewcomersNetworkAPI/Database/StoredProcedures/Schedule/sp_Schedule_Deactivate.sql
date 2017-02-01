CREATE PROCEDURE [dbo].[sp_Schedule_Deactivate]
@cServiceId nvarchar(128),
@cScheduleId nvarchar(128)

AS
BEGIN 
	UPDATE [dbo].[ServiceScheduleItem]
	SET [Status] = 'D'
	WHERE [ScheduleId] = @cScheduleId
	AND [Status] = 'O'
END

BEGIN
	UPDATE [dbo].[ServicesSchedule]
	SET [Status] = 'D'
	WHERE [ServiceId] = @cServiceId
	AND [Id] = @cScheduleId
	AND [Status] = 'O'
END
 