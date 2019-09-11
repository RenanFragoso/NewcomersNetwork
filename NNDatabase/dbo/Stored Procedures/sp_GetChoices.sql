CREATE PROCEDURE [dbo].[sp_GetChoices]
WITH EXECUTE AS CALLER
AS

SELECT [ChoiceName], [ChoiceGroup], [ChoiceSP]
FROM [dbo].[Choices]
WHERE [Status] = 'O'