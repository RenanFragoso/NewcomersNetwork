CREATE PROCEDURE [dbo].[sp_Services_GetAll]
AS
SELECT [ServiceID], [ServiceName], [ServiceDescription], [ServiceResponsible]
FROM [dbo].[Services]