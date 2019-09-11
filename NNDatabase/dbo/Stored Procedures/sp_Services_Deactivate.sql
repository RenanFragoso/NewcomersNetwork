CREATE PROCEDURE [dbo].[sp_Services_Deactivate]
@cServiceId nvarchar(128)

AS

UPDATE [dbo].[Services]
SET [ServiceStatus] = 'C' 
WHERE [ServiceId] = @cServiceId