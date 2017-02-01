CREATE PROCEDURE [dbo].[sp_GetChoices_ServicesGroupColors]
WITH EXECUTE AS CALLER
AS
SELECT [id], [text]
FROM [dbo].[Choices_GroupColors]
WHERE [Status] = 'O'