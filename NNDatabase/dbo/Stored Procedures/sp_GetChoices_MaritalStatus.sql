CREATE PROCEDURE [dbo].[sp_GetChoices_MaritalStatus]
WITH EXECUTE AS CALLER
AS
SELECT [ID] as Id, [Description] as Text 
FROM [dbo].[Choices_MaritalStatus]
WHERE [Status] = 'O'