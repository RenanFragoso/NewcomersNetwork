IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ClassifiedsGroups_GetByID]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_ClassifiedsGroups_GetByID]
GO

CREATE PROCEDURE [dbo].[sp_ClassifiedsGroups_GetByID]
	@cId nvarchar(128)
AS

SELECT [Id], [GroupName], [GroupDescription], [GroupColor], [GroupIcon], [GroupStatus], [CreateDate], [AlterDate], [TextColor]
FROM [dbo].[ClassifiedsGroups]
WHERE [Id] = @cId
AND [GroupStatus] = 'O'