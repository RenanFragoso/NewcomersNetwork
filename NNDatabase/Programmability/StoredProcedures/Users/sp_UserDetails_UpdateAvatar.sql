IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserDetails_UpdateAvatar]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_UserDetails_UpdateAvatar]
GO

CREATE PROCEDURE [dbo].[sp_UserDetails_UpdateAvatar]
@Id nvarchar(128),
@pictureName nvarchar(256)

AS

UPDATE [dbo].[UserDetails] 
	SET Picture = @pictureName
	WHERE Id = @Id