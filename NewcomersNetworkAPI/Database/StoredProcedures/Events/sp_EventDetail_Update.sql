CREATE PROCEDURE [dbo].[sp_EventDetail_Update]
@cId nvarchar(126), 
@cEventId nvarchar(126), 
@cTitle NVARCHAR(50), 
@cSubTitle NVARCHAR(100), 
@cText1 ntext, 
@cText2 ntext, 
@cFooter ntext, 
@cHeadImg nvarchar(256), 
@cLocation ntext

AS

UPDATE [dbo].[EventDetail] 
SET [EventId] = @cEventId, 
[Title] = @cTitle, 
[SubTitle] = @cSubTitle, 
[Text1] = @cText1, 
[Text2] = @cText2, 
[Footer] = @cFooter, 
[HeadImg] = @cHeadImg, 
[Location] = @cLocation
WHERE [Id] = @cId