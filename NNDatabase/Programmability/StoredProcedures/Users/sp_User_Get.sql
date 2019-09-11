IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_User_Get]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_User_Get]
GO

CREATE PROCEDURE [dbo].[sp_User_Get]
@cUserId nvarchar(128)

AS

SELECT [Id], [Email], [UserName], [SecurityStamp]
FROM [dbo].[AspNetUsers]
WHERE [Id] = @cUserId