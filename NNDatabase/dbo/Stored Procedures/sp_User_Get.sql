CREATE PROCEDURE [dbo].[sp_User_Get]
@cUserId nvarchar(128)

AS

SELECT [Id], [Email], [UserName]
FROM [dbo].[AspNetUsers]
WHERE [Id] = @cUserId