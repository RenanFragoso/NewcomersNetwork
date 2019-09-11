CREATE PROCEDURE [dbo].[sp_Events_GetFromDate]
@dateFrom datetime
AS
SELECT [Id], [Name], [Description], [StartDate], [EndDate], [Published], [StartPublishDate], [EndPublishDate], [Finished], [MaxSlots], [CurSlots], [Image], [CreatedBy], [Type], [ExternalLink], [Title], [SubTitle], [Text1], [Text2], [Footer], [HeadImg], [Location], [CreateDate], [AlterDate], [Status]
FROM [dbo].[Events]
WHERE [StartDate] >= @dateFrom
AND [Finished] = 0