CREATE PROCEDURE [dbo].[sp_Calendar_GetEvents]
@dStartDate datetime,
@dEndDate datetime

AS

SELECT [Id], [Name], [Description], [StartDate], [EndDate], [StartPublishDate], [EndPublishDate], [ExternalLink]
FROM [dbo].[Events] 
WHERE [StartDate] >= @dStartDate
AND [EndDate] <= @dEndDate
AND [Status] = 'O'