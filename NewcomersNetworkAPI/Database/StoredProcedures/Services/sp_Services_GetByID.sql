CREATE PROCEDURE [dbo].[sp_Services_GetByID]
@cServiceId nvarchar(128)
AS
SELECT *
FROM [dbo].[Services]
WHERE [ServiceID] = @cServiceId