CREATE PROCEDURE [dbo].[sp_GetChoices_ServicesGroups]
WITH EXECUTE AS CALLER
AS
SELECT [GroupId] AS id, [GroupName] AS 'text'
FROM [dbo].[ServicesGroup]
WHERE [GroupStatus] = 'O'