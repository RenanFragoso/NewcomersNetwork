CREATE PROCEDURE [dbo].[sp_Classifieds_Delete]
@cId nvarchar(128)

AS

DELETE FROM [dbo].[Classifieds] 
WHERE [Id] = @cId