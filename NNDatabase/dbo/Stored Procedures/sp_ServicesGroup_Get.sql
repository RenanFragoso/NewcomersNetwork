CREATE PROCEDURE [dbo].[sp_ServicesGroup_Get]
AS
SELECT *
FROM [dbo].[ServicesGroup]
WHERE [GroupStatus] = 'O'