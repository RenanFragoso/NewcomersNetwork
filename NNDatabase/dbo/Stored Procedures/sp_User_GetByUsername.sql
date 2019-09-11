CREATE PROCEDURE [dbo].[sp_User_GetByUsername]
@cUserName nvarchar(256)

AS

SELECT USR.[Id], USR.[Email], USR.[UserName], USR.[SecurityStamp]
FROM [dbo].[AspNetUsers] USR
INNER JOIN [dbo].[UserDetails] DET
ON ( DET.[Id] = USR.[Id] )
WHERE USR.[UserName] = @cUserName