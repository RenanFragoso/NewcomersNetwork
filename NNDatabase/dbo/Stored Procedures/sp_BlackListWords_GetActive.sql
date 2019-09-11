CREATE PROCEDURE [dbo].[sp_BlackListWords_GetActive]

AS

SELECT [Word], [Status]
FROM [dbo].[BlackListedWords]
WHERE [Status] = 'O'