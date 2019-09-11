IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Messages_Get]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Messages_Get]
GO

CREATE PROCEDURE [dbo].[sp_Messages_Get]
@cClassifiedId nvarchar(128),
@nId int 

AS

SELECT [Id], [ClassifiedId], [From], [To], [Date], [Message], [AlterDate], [Status], [ReplyTo]
FROM [dbo].[ClassifiedsMessages]
WHERE [ClassifiedId] = @cClassifiedId
AND [Id] = @nId