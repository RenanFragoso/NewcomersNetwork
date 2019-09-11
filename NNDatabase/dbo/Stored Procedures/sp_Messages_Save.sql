CREATE PROCEDURE [dbo].[sp_Messages_Save]
@cClassifiedId nvarchar(128),
@cFrom nvarchar(128),
@cTo nvarchar(128),
@dDate datetime,
@cMessage text,
@dAlterDate datetime,
@nReplyTo int

AS

DECLARE @NEWIDTBL Table (
	Id nvarchar(128)
);

INSERT INTO [dbo].[ClassifiedsMessages]
( [ClassifiedId], [From], [To], [Date], [Message], [AlterDate], [Status], [ReplyTo] )
OUTPUT Inserted.Id into @NEWIDTBL
VALUES ( @cClassifiedId, @cFrom, @cTo, @dDate, @cMessage, @dAlterDate, 'O', @nReplyTo )

SELECT Id AS LAST_MESSAGE FROM @NEWIDTBL