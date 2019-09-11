IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Messages_Save]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Messages_Save]
GO

CREATE PROCEDURE [dbo].[sp_Messages_Save]
@classifiedId nvarchar(128),
@from nvarchar(128),
@to nvarchar(128),
@date datetime,
@message text,
@alterDate datetime,
@replyTo int = 0

AS

DECLARE @NEWIDTBL Table (
	Id nvarchar(128)
);

INSERT INTO [dbo].[ClassifiedsMessages]
( [ClassifiedId], [From], [To], [Date], [Message], [AlterDate], [Status], [ReplyTo] )
OUTPUT Inserted.Id into @NEWIDTBL
VALUES ( @classifiedId, @from, @to, @date, @message, @alterDate, 'O', @replyTo)

SELECT Id AS LAST_MESSAGE FROM @NEWIDTBL