CREATE PROCEDURE [dbo].[sp_Messages_Get]
@cClassifiedId nvarchar(128),
@nId int 

AS

SELECT [Id], [ClassifiedId], [From], [To], [Date], [Message], [AlterDate], [Status], [ReplyTo]
FROM [dbo].[ClassifiedsMessages]
WHERE [ClassifiedId] = @cClassifiedId
AND [Id] = @nId