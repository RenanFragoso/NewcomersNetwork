CREATE PROCEDURE [dbo].[sp_Classifieds_PendingNum]

AS

SELECT count(*) as PENDING_NUM
FROM [dbo].[Classifieds] 
WHERE [Status] = 'P'