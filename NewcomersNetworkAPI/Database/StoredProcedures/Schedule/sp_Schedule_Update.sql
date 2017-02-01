CREATE PROCEDURE [dbo].[sp_Schedule_Update]
@cServiceId nvarchar(128),
@cId nvarchar(128),
@cName nvarchar(50),
@cDescription nvarchar(100),
@dStartDate datetime,
@dEndDate datetime,
@cResponsible nvarchar(128),
@bUniqueInscription bit,
@bRequireRegister bit,
@dAlterDate datetime

AS

UPDATE [dbo].[ServicesSchedule]

SET 
[Name] = @cName,
[Description] = @cDescription,
[StartDate] = @dStartDate,
[EndDate] = @dEndDate,
[Responsible] = @cResponsible,
[UniqueInscription] = @bUniqueInscription,
[RequireRegister] = @bRequireRegister,
[AlterDate] = @dAlterDate
WHERE [ServiceId] = @cServiceId
AND [Id] = @cId