CREATE PROCEDURE [dbo].[sp_Classifieds_GetByID]
@cId nvarchar(128)
AS
SELECT *
FROM [dbo].[Classifieds]
WHERE [Id] = @cId