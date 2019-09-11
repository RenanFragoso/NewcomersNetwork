CREATE PROCEDURE [dbo].[sp_User_Delete]
@cUserId nvarchar(128)

AS

DELETE FROM [dbo].[UserDetails]
WHERE [Id] = @cUserId