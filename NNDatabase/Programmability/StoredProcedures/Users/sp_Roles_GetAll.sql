CREATE PROCEDURE [dbo].[sp_Roles_GetAll]

AS

SELECT [Id], [Name]
FROM [dbo].[AspNetRoles]