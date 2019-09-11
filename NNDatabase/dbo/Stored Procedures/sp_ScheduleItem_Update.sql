CREATE PROCEDURE [dbo].[sp_ScheduleItem_Update]
@nId int,
@cScheduleId nvarchar(128),
@dStartDate datetime,
@dEndDate datetime,
@nSlots int,
@nMaxSlots int,
@dAlterDate datetime

AS

UPDATE [dbo].[ServiceScheduleItem]
SET [StartDate] = @dStartDate,
[EndDate] = @dEndDate,
[Slots] = @nSlots,
[MaxSlots] = @nMaxSlots,
[AlterDate] = @dAlterDate
WHERE [ScheduleId] = @cScheduleId
AND [Id] = @nId