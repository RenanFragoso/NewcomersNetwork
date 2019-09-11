CREATE PROCEDURE [dbo].[sp_User_GetByEmail]
@cEmail nvarchar(100)

AS

SELECT USR.[Id], USR.[Email], USR.[UserName], USR.[SecurityStamp]
FROM [dbo].[AspNetUsers] USR
INNER JOIN [dbo].[UserDetails] DET
ON ( DET.[Id] = USR.[Id] )
WHERE USR.[Email] = @cEmail