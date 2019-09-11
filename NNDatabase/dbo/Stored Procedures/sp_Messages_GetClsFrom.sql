CREATE PROCEDURE [dbo].[sp_Messages_GetClsFrom]
@cClassifiedId nvarchar(128),
@cFrom nvarchar(128)

AS

SELECT MSG.[Id], MSG.[ClassifiedId], MSG.[From], MSG.[To], MSG.[Date], MSG.[Message], MSG.[AlterDate], MSG.[Status], MSG.[ReplyTo]
FROM [dbo].[ClassifiedsMessages] as MSG
LEFT JOIN [dbo].[Classifieds] as CLS
ON ( CLS.[Id] = MSG.[ClassifiedId] )
WHERE [ClassifiedId] = @cClassifiedId
AND (MSG.[From] = @cFrom OR MSG.[To] = @cFrom OR ( MSG.[From] = CLS.[CreatedBy] AND MSG.[ReplyTo] = 0) )
AND MSG.[Status] = 'O'
ORDER BY MSG.[ReplyTo],MSG.[Date] ASC