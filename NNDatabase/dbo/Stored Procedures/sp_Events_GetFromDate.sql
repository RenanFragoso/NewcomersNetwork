CREATE PROCEDURE [dbo].[sp_Events_GetFromDate]
@dateFrom datetime
AS
SELECT [Id], [Name], [Description], [StartDate], [EndDate], [Published], [StartPublishDate], [EndPublishDate], [Finished], [MaxSlots], [CurSlots], [Image], [CreatedBy], [Type] 
FROM [dbo].[Events]
WHERE [StartDate] >= @dateFrom
AND [Finished] = 0