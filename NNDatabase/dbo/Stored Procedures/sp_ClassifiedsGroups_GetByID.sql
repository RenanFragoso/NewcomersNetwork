CREATE PROCEDURE [dbo].[sp_ClassifiedsGroups_GetByID]
@cId nvarchar(128)
AS
SELECT [Id], [GroupName], [GroupDescription], [GroupColor], [GroupIcon], [GroupStatus], [CreateDate], [AlterDate]
FROM [dbo].[ClassifiedsGroups]
WHERE [Id] = @cId
AND [GroupStatus] = 'O'