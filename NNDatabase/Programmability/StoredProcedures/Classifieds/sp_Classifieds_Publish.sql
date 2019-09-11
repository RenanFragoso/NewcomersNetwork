CREATE PROCEDURE [dbo].[sp_Classifieds_Publish]
@cId nvarchar(128)

AS

UPDATE [dbo].[Classifieds] 
SET [Status] = 'O'
WHERE [Id] = @cId