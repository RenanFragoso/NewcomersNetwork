CREATE PROCEDURE [dbo].[sp_Services_Get]
@cServiceId nvarchar(128)

AS

SELECT *
FROM [dbo].[Services]
WHERE [ServiceID] = @cServiceId