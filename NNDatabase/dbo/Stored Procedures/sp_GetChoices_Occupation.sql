CREATE PROCEDURE [dbo].[sp_GetChoices_Occupation]
WITH EXECUTE AS CALLER
AS
SELECT *
FROM dbo.Choices_Occupation
WHERE Status='O'