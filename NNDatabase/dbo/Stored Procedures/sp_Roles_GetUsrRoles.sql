CREATE PROCEDURE [dbo].[sp_Roles_GetUsrRoles]
@cUserId nvarchar(128)

AS

SELECT [RoleId]
FROM [dbo].[AspNetUserRoles]
WHERE [UserId] = @cUserId