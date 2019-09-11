CREATE PROCEDURE [dbo].[sp_AllMessages_GetFrom]
@cFrom nvarchar(128)

AS

SELECT [Id], [ClassifiedId], [From], [To], [Date], [Message], [AlterDate], [Status]
FROM [dbo].[ClassifiedsMessages]
WHERE ([From] = @cFrom OR [To] = @cFrom)
AND [Status] = 'O'
ORDER BY [Date]