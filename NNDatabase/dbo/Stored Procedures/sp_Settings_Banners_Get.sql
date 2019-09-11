CREATE PROCEDURE [dbo].[sp_Settings_Banners_Get]
@startDate datetime,
@endDate datetime

AS

SELECT [Id], [Link], [Title1], [Title2], [Status]
FROM [dbo].[Settings_Banners] 
WHERE [StartPublishing] <= @startDate
AND [EndPublishing] <= @endDate
AND [Status] = 'O'