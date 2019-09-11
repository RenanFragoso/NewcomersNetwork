CREATE PROCEDURE [dbo].[sp_Classifieds_Pending]

AS

SELECT *
FROM [dbo].[Classifieds] 
WHERE [Status] = 'P'