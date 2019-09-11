CREATE PROCEDURE [dbo].[sp_Events_GetFromPublishDate]
@dateFrom datetime
AS
SELECT [Id], [Name], [Description], [StartDate], [EndDate], [Published], [StartPublishDate], [EndPublishDate], [Finished], [MaxSlots], [CurSlots], [Image], [CreatedBy], [Type], [ExternalLink], [Title], [SubTitle], [Text1], [Text2], [Footer], [HeadImg], [Location], [CreateDate], [AlterDate], [Status]
FROM [dbo].[Events]
WHERE [StartPublishDate] <= @dateFrom
AND [EndPublishDate] >= @dateFrom
AND [Finished] = 0
ORDER BY [StartDate]