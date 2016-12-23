CREATE PROCEDURE [dbo].[sp_Services_Insert]
@cServiceName NVARCHAR(50)=NULL,
@cServiceDescription NVARCHAR(1000)=NULL, 
@cServiceResponsible nvarchar(126),
@cServiceGroup nvarchar(126),
@dServiceCreateDate datetime,
@dServiceAlterDate datetime

AS

INSERT INTO [dbo].[Services] 
( [ServiceName], [ServiceDescription], [ServiceResponsible], [ServiceGroup], [ServiceCreateDate], [ServiceAlterDate] )
VALUES ( @cServiceName, @cServiceDescription, @cServiceResponsible, @cServiceGroup, @dServiceCreateDate, @dServiceAlterDate )

SELECT SCOPE_IDENTITY() AS LAST_SERVICE