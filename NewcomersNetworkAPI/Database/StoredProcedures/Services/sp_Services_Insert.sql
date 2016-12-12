CREATE PROCEDURE [dbo].[sp_Services_Insert]
@ServiceName NVARCHAR(50)=NULL,
@ServiceDescription NVARCHAR(1000)=NULL, 
@ServiceResponsible int,
@ServiceCreateDate datetime,
@ServiceAlterDate datetime

AS

INSERT INTO [dbo].[Services] 
( ServiceName, ServiceDescription, ServiceResponsible, ServiceCreateDate, ServiceAlterDate )
VALUES ( @ServiceName, @ServiceDescription, @ServiceResponsible, @ServiceCreateDate, @ServiceAlterDate )

SELECT SCOPE_IDENTITY() AS LAST_SERVICE