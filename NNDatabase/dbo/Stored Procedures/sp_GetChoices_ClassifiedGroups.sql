CREATE PROCEDURE [dbo].[sp_GetChoices_ClassifiedGroups]

AS

SELECT [Id] AS 'id', [GroupName] AS 'text'
FROM [dbo].[ClassifiedsGroups]
WHERE [GroupStatus] = 'O'