CREATE PROCEDURE [dbo].[sp_Schedule_Activate]
@cServiceId nvarchar(128),
@cScheduleId nvarchar(128)

AS
BEGIN 
	UPDATE [dbo].[ServiceScheduleItem]
	SET [Status] = 'O'
	WHERE [ScheduleId] = @cScheduleId
	AND [Status] = 'D'
END

BEGIN
	UPDATE [dbo].[ServicesSchedule]
	SET [Status] = 'O'
	WHERE [ServiceId] = @cServiceId
	AND [Id] = @cScheduleId
	AND [Status] = 'D'
END