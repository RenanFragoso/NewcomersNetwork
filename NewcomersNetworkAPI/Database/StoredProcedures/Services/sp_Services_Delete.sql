CREATE PROCEDURE [dbo].[sp_Services_Delete]
@cServiceId nvarchar(128)

AS

DELETE FROM [dbo].[Services] 
WHERE [ServiceID] = @cServiceId