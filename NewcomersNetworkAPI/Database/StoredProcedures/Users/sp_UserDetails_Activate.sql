CREATE PROCEDURE [dbo].[sp_UserDetails_Activate]
@cId nvarchar(128),
@dModify datetime

AS

UPDATE [dbo].[UserDetails] 
SET [Status] = 'O',
	[LastModified] = @dModify
WHERE Id = @cId