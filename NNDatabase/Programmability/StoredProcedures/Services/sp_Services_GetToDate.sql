CREATE PROCEDURE [dbo].[sp_Service_GetToDate]
@dateToFind datetime
AS
SELECT *
FROM [dbo].[Services]
WHERE [ServiceStartDate] <= @dateToFind
AND [ServiceEndDate] >= @dateToFind
AND [ServiceStatus] = 'O';