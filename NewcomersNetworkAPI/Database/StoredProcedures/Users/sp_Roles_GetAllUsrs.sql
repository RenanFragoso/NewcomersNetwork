CREATE PROCEDURE [dbo].[sp_Roles_GetAllUsrs]
@cRoleId nvarchar(128)

AS

SELECT [USRXROLE].[UserId], [USRXROLE].[RoleId], [ROLES].[Name]
FROM [dbo].[AspNetUserRoles] USRXROLE
INNER JOIN [dbo].[AspNetRoles] ROLES
ON ( [ROLES].[Id] = [USRXROLE].[RoleId] )
WHERE [USRXROLE].[RoleId] = @cRoleId