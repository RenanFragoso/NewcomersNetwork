CREATE PROCEDURE [dbo].[sp_Services_Delete]
@cServiceId nvarchar(126)

AS

DELETE FROM [dbo].[Services] 
WHERE [ServiceID] = @cServiceId