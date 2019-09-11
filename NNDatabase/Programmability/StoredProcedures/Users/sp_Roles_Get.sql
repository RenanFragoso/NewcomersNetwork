CREATE PROCEDURE [dbo].[sp_Roles_Get]
@cRoleId nvarchar(128)

AS

SELECT [Id], [Name]
FROM [dbo].[AspNetRoles]
WHERE [Id] = @cRoleId