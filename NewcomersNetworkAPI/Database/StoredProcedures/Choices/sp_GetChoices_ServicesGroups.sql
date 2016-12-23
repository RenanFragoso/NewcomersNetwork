CREATE PROCEDURE [dbo].[sp_GetChoices_ServicesGroups]
WITH EXECUTE AS CALLER
AS
SELECT [GroupId] as Id, [GroupName] as Text
FROM [dbo].[ServicesGroup]
WHERE [GroupStatus] = 'O'