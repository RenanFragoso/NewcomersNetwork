CREATE PROCEDURE [dbo].[sp_ServicesGroup_GetAll]
AS
SELECT *
FROM [dbo].[ServicesGroup]
WHERE [Hidden] = 'N'