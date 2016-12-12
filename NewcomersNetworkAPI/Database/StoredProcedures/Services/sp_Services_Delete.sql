CREATE PROCEDURE [dbo].[sp_Services_Delete]
@ServiceID int

AS

DELETE FROM [dbo].[Services] 
WHERE [ServiceID] = @ServiceID