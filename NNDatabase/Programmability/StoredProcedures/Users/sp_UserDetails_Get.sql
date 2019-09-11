IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserDetails_Get]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_UserDetails_Get]
GO

CREATE PROCEDURE [dbo].[sp_UserDetails_Get]
@cUserId nvarchar(128)

AS

SELECT *
FROM [dbo].[UserDetails]
WHERE [Id] = @cUserId


