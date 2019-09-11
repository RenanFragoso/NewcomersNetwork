IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Widgets_Banners_Get]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Widgets_Banners_Get]
GO

CREATE PROCEDURE [dbo].[sp_Widgets_Banners_Get]
@startDate datetime,
@endDate datetime

AS

SELECT [Id]
FROM [dbo].[Widgets_Banners] 
WHERE [StartPublishing] <= @startDate
AND [EndPublishing] <= @endDate
AND [Status] = 'O'