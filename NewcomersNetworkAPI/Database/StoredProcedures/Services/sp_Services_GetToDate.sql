CREATE PROCEDURE [dbo].[sp_Events_GetToDate]
@dateToFind datetime
AS
SELECT *
FROM [dbo].[Services]
WHERE [EventStartDate] <= @dateToFind
AND [EventEndDate] >= @dateToFind