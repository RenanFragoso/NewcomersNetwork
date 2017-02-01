CREATE PROCEDURE [dbo].[sp_GetChoices_GroupIcons]
WITH EXECUTE AS CALLER
AS
SELECT [id], [text]
FROM [dbo].[Choices_GroupIcons]
WHERE [Status] = 'O'