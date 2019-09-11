CREATE PROCEDURE [dbo].[sp_BlackListWords_GetAll]

AS

SELECT [Word], [Status]
FROM [dbo].[BlackListedWords]
