CREATE PROCEDURE [dbo].[sp_GetChoices_AgeRange]
WITH EXECUTE AS CALLER
AS
SELECT [ID] as Id, Description as Text
FROM [dbo].[Choices_AgeRange]
WHERE [Status] = 'O'