CREATE PROCEDURE [dbo].[sp_GetChoices_LevelOfEnglish]
WITH EXECUTE AS CALLER
AS
SELECT * FROM Choices_LevelOfEnglish
WHERE Status = 'O'