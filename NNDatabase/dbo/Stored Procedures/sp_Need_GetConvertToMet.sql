CREATE PROCEDURE [dbo].[sp_Need_GetConvertToMet]
@MaxDaysUntilConvert INT
AS
SELECT *
FROM Need
WHERE State='P' AND DATEDIFF(day, DatePending, GETDATE()) > @MaxDaysUntilConvert