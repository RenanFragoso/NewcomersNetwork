CREATE PROCEDURE [dbo].[sp_Classifieds_GetAll]
AS
SELECT *
FROM [dbo].[Classifieds]
WHERE [Status] = 'O'