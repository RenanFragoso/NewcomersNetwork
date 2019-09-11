CREATE PROCEDURE [dbo].[sp_GetChoices_ImmigrantCategory]
AS
SELECT * FROM [dbo].[Choices_ImmigrantCategory]
WHERE [Status] = 'O'