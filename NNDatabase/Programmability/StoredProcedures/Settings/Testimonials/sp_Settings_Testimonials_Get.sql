CREATE PROCEDURE [dbo].[sp_Settings_Banners_Get]

AS

SELECT [Id], 
FROM [dbo].[Testimonials] 
WHERE [Status] = 'O'