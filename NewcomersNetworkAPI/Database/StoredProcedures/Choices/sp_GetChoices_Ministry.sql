CREATE PROCEDURE [dbo].[sp_GetChoices_Ministry]
WITH EXECUTE AS CALLER
AS
SELECT *
FROM dbo.Ministry
WHERE Status='O'