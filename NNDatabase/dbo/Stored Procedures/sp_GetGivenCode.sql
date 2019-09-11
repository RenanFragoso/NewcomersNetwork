CREATE PROCEDURE [dbo].[sp_GetGivenCode]
WITH EXECUTE AS CALLER
AS
SELECT GivenCode
FROM dbo.SecretGivenCode
WHERE Status='O'