CREATE PROCEDURE [dbo].[sp_Services_Insert]
@cServiceName NVARCHAR(50)=NULL,
@cServiceDescription NVARCHAR(1000)=NULL, 
@cServiceGroup nvarchar(128),
@dServiceCreateDate datetime

AS

DECLARE @NEWIDTBL Table (
	Id nvarchar(128)
);

INSERT INTO [dbo].[Services] 
( [ServiceName], [ServiceDescription], [ServiceGroup], [ServiceStatus], [ServiceCreateDate], [ServiceAlterDate] )
OUTPUT Inserted.ServiceID into @NEWIDTBL
VALUES ( @cServiceName, @cServiceDescription, @cServiceGroup, 'O', @dServiceCreateDate, @dServiceCreateDate )

SELECT Id AS LAST_SERVICE FROM @NEWIDTBL