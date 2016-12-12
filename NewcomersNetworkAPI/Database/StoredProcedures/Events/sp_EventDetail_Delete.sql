CREATE PROCEDURE [dbo].[sp_EventDetail_Delete]
@cId int

AS

DELETE FROM [dbo].[EventDetail] 
WHERE [Id] = @cId