CREATE PROCEDURE [dbo].[sp_GetChoices_ClassifiedGroups]

AS

SELECT [Id], [GroupName] as Text
FROM [dbo].[ClassifiedsGroups]
WHERE [GroupStatus] = 'O'