IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Events_GetFromPublishDate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Events_GetFromPublishDate]
GO

CREATE PROCEDURE [dbo].[sp_Events_GetFromPublishDate]
@startDate datetime,
@endDate datetime

AS

SELECT *
FROM [dbo].[Events]
WHERE [StartPublishDate] <= @startDate
AND [EndPublishDate] <= @endDate
AND [Finished] = 0
ORDER BY [StartDate] ASC