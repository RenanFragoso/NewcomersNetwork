CREATE PROCEDURE [dbo].[sp_Schedule_Insert]
@cServiceId nvarchar(128),
@cName nvarchar(50),
@cDescription nvarchar(100),
@dStartDate datetime,
@dEndDate datetime,
@cResponsible nvarchar(128),
@bUniqueInscription bit,
@bRequireRegister bit,
@dCreateDate datetime

AS

DECLARE @NEWIDTBL Table (
	Id nvarchar(128)
);

INSERT INTO [dbo].[ServicesSchedule]
([ServiceId], [Name],[Description],[StartDate],[EndDate],[Responsible],[UniqueInscription],[RequireRegister],[CreateDate],[AlterDate])
OUTPUT Inserted.Id into @NEWIDTBL
VALUES (@cServiceId, @cName, @cDescription, @dStartDate, @dEndDate, @cResponsible, @bUniqueInscription, @bRequireRegister, @dCreateDate, @dCreateDate)
SELECT Id AS LAST_SCHEDULE FROM @NEWIDTBL