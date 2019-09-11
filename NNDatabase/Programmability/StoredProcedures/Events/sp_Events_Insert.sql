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
@cCreatedBy nvarchar(128), 
@nType int=0,
@dCreateDate datetime,
@cExternalLink nvarchar(256)='',
@cTitle nvarchar(50)='',
@cSubTitle nvarchar(100)='',
@cText1 ntext = '',
@cText2 ntext = '',
@cFooter ntext = '',
@cHeadImg nvarchar(256) = '',
@cLocation ntext = ''

AS

DECLARE @NEWIDTBL Table (
	Id nvarchar(128)
);

INSERT INTO [dbo].[Events]
([Name], [Description], [StartDate], [EndDate], [Published], [StartPublishDate], [EndPublishDate], [Finished], [MaxSlots], [CurSlots], [Image], [CreatedBy], [Type], [CreateDate], [AlterDAte], [ExternalLink], [Title], [SubTitle], [Text1], [Text2],[Footer], [HeadImg], [Location])
OUTPUT Inserted.Id into @NEWIDTBL
VALUES ( @cName, @cDescription, @dStartDate, @dEndDate, @bPublished, @dStartPublishDate, @dEndPublishDate, @bFinished, @nMaxSlots, @nCurSlots, @cImage, @cCreatedBy, @nType, @dCreateDate, @dCreateDate, @cExternalLink, @cTitle, @cSubTitle, @cText1, @cText2, @cFooter, @cHeadImg, @cLocation)

SELECT Id AS LAST_EVENT FROM @NEWIDTBL