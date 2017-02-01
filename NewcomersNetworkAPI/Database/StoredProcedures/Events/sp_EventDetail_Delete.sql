CREATE PROCEDURE [dbo].[sp_EventDetail_Delete]
@cId nvarchar(128)

AS

DELETE FROM [dbo].[EventDetail] 
WHERE [Id] = @cId