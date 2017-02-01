CREATE PROCEDURE [dbo].[sp_Services_Update]
@cServiceId nvarchar(126),
@cServiceName NVARCHAR(50)=NULL, 
@cServiceDescription NVARCHAR(1000)=NULL,
@cServiceGroup nvarchar(128),
@dServiceAlterDate datetime

AS

UPDATE [dbo].[Services] 
SET [ServiceName] = @cServiceName, 
[ServiceDescription] = @cServiceDescription, 
[ServiceGroup] = @cServiceGroup,
[ServiceAlterDate] = @dServiceAlterDate
WHERE [ServiceID] = @cServiceId