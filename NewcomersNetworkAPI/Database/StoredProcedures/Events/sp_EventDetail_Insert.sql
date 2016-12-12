CREATE PROCEDURE [dbo].[sp_EventDetail_Insert]
@cEventId nvarchar(128), 
@cTitle NVARCHAR(50), 
@cSubTitle NVARCHAR(100), 
@cText1 ntext, 
@cText2 ntext, 
@cFooter ntext, 
@cHeadImg nvarchar(256), 
@cLocation ntext

AS

INSERT INTO [dbo].[EventDetail] 
( [EventId], [Title], [SubTitle], [Text1], [Text2], [Footer], [HeadImg], [Location] ) 
VALUES ( @cEventId, @cTitle, @cSubTitle, @cText1, @cText2, @cFooter, @cHeadImg, @cLocation )

SELECT SCOPE_IDENTITY() AS LAST_DETAIL