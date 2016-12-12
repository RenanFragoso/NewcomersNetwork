CREATE PROCEDURE [dbo].[sp_Services_Update]
@ServiceID int,
@ServiceName NVARCHAR(50)=NULL, 
@ServiceDescription NVARCHAR(1000)=NULL, 
@ServiceResponsible int,
@ServiceAlterDate datetime

AS

UPDATE [dbo].[Services] 
SET [ServiceName] = @ServiceName, 
[ServiceDescription] = @ServiceDescription, 
[ServiceResponsible] = @ServiceResponsible,
[ServiceAlterDate] = @ServiceAlterDate
WHERE [ServiceID] = @ServiceID