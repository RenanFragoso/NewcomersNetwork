CREATE PROCEDURE [dbo].[sp_Messages_GetAll]
@cClassifiedId nvarchar(128)
AS

SELECT [Id], [ClassifiedId], [From], [To], [Date], [Message], [AlterDate], [Status], [ReplyTo]
FROM [dbo].[ClassifiedsMessages]
WHERE [ClassifiedId] = @cClassifiedId
AND [Status] = 'O'
ORDER BY [ReplyTo],[Date] ASC