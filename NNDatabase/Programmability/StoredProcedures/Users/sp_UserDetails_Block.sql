CREATE PROCEDURE [dbo].[sp_UserDetails_Block]
@cId nvarchar(128),
@dModify datetime

AS

UPDATE [dbo].[UserDetails] 
SET [Status] = 'C',
	[LastModified] = @dModify
WHERE Id = @cId