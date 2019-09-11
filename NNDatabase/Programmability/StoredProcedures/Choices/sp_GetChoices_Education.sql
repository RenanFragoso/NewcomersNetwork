CREATE PROCEDURE [dbo].[sp_GetChoices_Education]
WITH EXECUTE AS CALLER

AS

SELECT [ID] as Id, [Description] as Text 
FROM [dbo].[Choices_Education]
WHERE [Status] = 'O'