CREATE PROCEDURE [dbo].[sp_BlackListWords_AddWord]
@cWord as nvarchar(50)

AS

INSERT INTO [dbo].[BlackListedWords]
([Word], [Status])
VALUES ( @cWord, 'O')