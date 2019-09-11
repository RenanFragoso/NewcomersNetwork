CREATE PROCEDURE [dbo].[sp_GetChoices_UsersRoles]

AS

SELECT [Id], [Name] as Text
FROM [dbo].[AspNetRoles]