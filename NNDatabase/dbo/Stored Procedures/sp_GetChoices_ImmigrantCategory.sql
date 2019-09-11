CREATE PROCEDURE [dbo].[sp_GetChoices_ImmigrantCategory]
AS
SELECT * FROM Choices_ImmigrantCategory
WHERE Status = 'O'