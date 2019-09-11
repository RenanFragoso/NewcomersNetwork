CREATE PROCEDURE [dbo].[sp_BlackListWords_DeleteWord]
@cWord as nvarchar(50)

AS

DELETE FROM [dbo].[BlackListedWords]
WHERE [Word] = @cWord