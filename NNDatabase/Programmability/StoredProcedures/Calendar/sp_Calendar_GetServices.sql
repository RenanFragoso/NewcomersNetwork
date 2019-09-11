CREATE PROCEDURE [dbo].[sp_Calendar_GetServices]
@dStartDate datetime,
@dEndDate datetime

AS

SELECT [svc].[ServiceID],[svc].[ServiceName],[svc].[ServiceDescription],
[sch].[Id] AS ScheduleId,
[itm].[Id] as ItemId,[itm].[StartDate], [itm].[EndDate], [itm].[Slots], [itm].[MaxSlots],
[grp].[GroupId], [grp].[GroupDescription], [grp].[GroupIcon], [grp].[GroupColor],
[sch].[Responsible] AS ResponsibleId, CONCAT([usr].[FirstName], ' ', [usr].[LastName]) AS Responsible

FROM [dbo].[ServiceScheduleItem] itm

INNER JOIN [ServicesSchedule] sch 
ON ( [sch].[Id] = [itm].[ScheduleId]
AND [sch].[Status] = 'O' )

INNER JOIN [Services] svc
ON ([svc].[ServiceID] = [sch].[ServiceId]
AND [svc].[ServiceStatus] = 'O' )

INNER JOIN [ServicesGroup] grp
ON ( [grp].[GroupId] = [svc].[ServiceGroup]
AND [grp].[GroupStatus] = 'O' )

INNER JOIN [UserDetails] usr
ON ( [usr].[Id] = [sch].[Responsible])

WHERE [itm].[StartDate] >= @dStartDate
AND [itm].[EndDate] <= @dEndDate
AND [itm].[Status] = 'O'