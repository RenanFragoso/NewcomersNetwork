CREATE PROCEDURE [dbo].[sp_GetChoices_Gender]
WITH EXECUTE AS CALLER
AS
SELECT [ID] as Id, [Description] as Text 
FROM [dbo].[Choices_Gender]
WHERE [Status] = 'O'