CREATE PROCEDURE [dbo].[sp_Need_TotalMetCount]
AS
SELECT COUNT(ID) as 'Total'
FROM dbo.Need
WHERE State='M'