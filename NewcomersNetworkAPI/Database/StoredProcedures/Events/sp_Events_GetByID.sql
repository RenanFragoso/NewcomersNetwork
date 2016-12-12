CREATE PROCEDURE [dbo].[sp_Events_GetByID]
@cId INT
AS
SELECT [Id], [Name], [Description], [StartDate], [EndDate], [Published], [StartPublishDate], [EndPublishDate], [Finished], [MaxSlots], [CurSlots], [Image], [CreatedBy], [Type], [ExternalLink]
FROM [dbo].[Events]
WHERE [Id] = @cId