CREATE PROCEDURE [dbo].[sp_Services_GetByID]
@ServiceIDIn INT
AS
SELECT [ServiceID], [ServiceName], [ServiceDescription], [ServiceResponsible]
FROM [dbo].[Services]
WHERE [ServiceID] = @ServiceIDIn