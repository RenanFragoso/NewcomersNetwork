CREATE PROCEDURE [dbo].[sp_Events_Update]
@cId nvarchar(128),
@cName NVARCHAR(50)=NULL, 
@cDescription NVARCHAR(1000)=NULL, 
@dStartDate datetime, 
@dEndDate datetime, 
@bPublished bit=0, 
@dPublishDate date, 
@dStartPublishDate datetime,
@dEndPublishDate datetime, 
@bFinished bit=0, 
@nMaxSlots int=0, 
@nCurSlots int=0, 
@cImage NVARCHAR(255)=NULL, 
@cCreatedBy nvarchar(128), 
@nType int=0,
@dAlterDate datetime,
@cEsternalLink nvarchar(256)

AS

UPDATE [dbo].[Events] 
SET [Name] = @cName, 
[Description] = @cDescription, 
[StartDate] = @dStartDate, 
[EndDate] = @dEndDate, 
[Published] = @bPublished, 
[StartPublishDate] = @dStartPublishDate, 
[EndPublishDate] = @dEndPublishDate, 
[Finished] = @bFinished, 
[MaxSlots] = @nMaxSlots, 
[CurSlots] = @nCurSlots, 
[Image] = @cImage, 
[CreatedBy] = @cCreatedBy, 
[Type] = @nType,
[AlterDate] = @dAlterDate,
[ExternalLink] = @cEsternalLink
WHERE [Id] = @cId