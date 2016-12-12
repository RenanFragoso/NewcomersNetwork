CREATE PROCEDURE [dbo].[sp_Events_Insert]
@cName NVARCHAR(50)=NULL, 
@cDescription NVARCHAR(1000)=NULL, 
@dStartDate datetime, 
@dEndDate datetime, 
@bPublished int=0, 
@dStartPublishDate date, 
@dEndPublishDate date, 
@bFinished bit = 0, 
@nMaxSlots int=0, 
@nCurSlots int=0, 
@cImage NVARCHAR(255)=NULL, 
@cCreatedBy nvarchar(126), 
@nType int=0,
@dCreateDate datetime,
@dAlterDate datetime,
@cExternalLink nvarchar(256)

AS

INSERT INTO [dbo].[Events] 
( [Name], [Description], [StartDate], [EndDate], [Published], [StartPublishDate], [EndPublishDate], [Finished], [MaxSlots], [CurSlots], [Image], [CreatedBy], [Type], [CreateDate], [AlterDAte], [ExternalLink] ) 
VALUES ( @cName, @cDescription, @dStartDate, @dEndDate, @bPublished, @dStartPublishDate, @dEndPublishDate, @bFinished, @nMaxSlots, @nCurSlots, @cImage, @cCreatedBy, @nType, @dCreateDate, @dAlterDAte, @cExternalLink)

SELECT SCOPE_IDENTITY() AS LAST_EVENT