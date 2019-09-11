CREATE PROCEDURE [dbo].[sp_Messages_GetClsFromTo]
@cClassifiedId nvarchar(128),
@cFrom nvarchar(128),
@cTo nvarchar(128)

AS

SELECT [Id], [ClassifiedId], [From], [To], [Date], [Message], [AlterDate], [Status], [ReplyTo]
FROM [dbo].[ClassifiedsMessages]
WHERE [ClassifiedId] = @cClassifiedId
AND [From] = @cFrom
AND [To] = @cTo
AND [Status] = 'O'
ORDER BY [ReplyTo],[Date] ASC