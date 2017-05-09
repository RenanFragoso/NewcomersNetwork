CREATE PROCEDURE [dbo].[sp_ClassifiedsGroups_GetAll]
AS
SELECT *
FROM [dbo].[ClassifiedsGroups]
WHERE [GroupStatus] = 'O'