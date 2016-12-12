CREATE PROCEDURE [dbo].[sp_Events_GetAll]
AS
SELECT [Id], [Name], [Description], [StartDate], [EndDate], [Published], [StartPublishDate], [EndPublishDate], [Finished], [MaxSlots], [CurSlots], [Image], [CreatedBy], [Type], [ExternalLink], [CreateDate], [AlterDate]
FROM [dbo].[Events]