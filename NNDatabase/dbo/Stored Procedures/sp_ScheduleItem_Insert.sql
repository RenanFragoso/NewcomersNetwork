CREATE PROCEDURE [dbo].[sp_ScheduleItem_Insert]
@cScheduleId nvarchar(128),
@dStartDate datetime,
@dEndDate datetime,
@nMaxSlots int,
@dCreateDate datetime

AS

DECLARE @NEWIDTBL Table (
	Id int
);

INSERT INTO [dbo].[ServiceScheduleItem]
([ScheduleId],[StartDate],[EndDate],[Slots],[MaxSlots],[CreateDate],[AlterDate])
OUTPUT Inserted.Id into @NEWIDTBL
VALUES (@cScheduleId,@dStartDate,@dEndDate,0,@nMaxSlots,@dCreateDate,@dCreateDate)
SELECT Id AS LAST_SCHDITM FROM @NEWIDTBL