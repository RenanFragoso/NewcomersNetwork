CREATE PROCEDURE [dbo].[sp_Events_Delete]
@cId int

AS

DELETE FROM [dbo].[Events] 
WHERE [Id] = @cId