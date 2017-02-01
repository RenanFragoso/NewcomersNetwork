CREATE PROCEDURE [dbo].[sp_Events_Delete]
@cId nvarchar(128)

AS

DELETE FROM [dbo].[Events] 
WHERE [Id] = @cId