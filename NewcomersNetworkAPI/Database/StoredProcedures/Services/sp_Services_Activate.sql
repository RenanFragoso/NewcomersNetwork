CREATE PROCEDURE [dbo].[sp_Services_Activate]
@cServiceId nvarchar(128)

AS

UPDATE [dbo].[Services]
SET [ServiceStatus] = 'O' 
WHERE [ServiceId] = @cServiceId