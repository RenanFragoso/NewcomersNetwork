CREATE PROCEDURE [dbo].[sp_Messages_GetClsTo]
@cClassifiedId nvarchar(128),
@cTo nvarchar(128)

AS

SELECT [Id], [ClassifiedId], [From], [To], [Date], [Message], [AlterDate], [Status], [ReplyTo]
FROM [dbo].[ClassifiedsMessages]
WHERE [ClassifiedId] = @cClassifiedId
AND [To] = @cTo
AND [Status] = 'O'
ORDER BY [Date]