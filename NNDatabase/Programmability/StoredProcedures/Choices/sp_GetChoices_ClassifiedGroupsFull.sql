IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetChoices_ClassifiedGroupsFull]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_GetChoices_ClassifiedGroupsFull]
GO

CREATE PROCEDURE [dbo].[sp_GetChoices_ClassifiedGroupsFull]

AS

SELECT [Id] AS 'id', CONCAT( '{ "Id": "', [Id], '", "GroupName": "', [GroupName], '", "GroupColor": "', [GroupColor], '", "GroupIcon": "', [GroupIcon], '", "GroupDescription": "', [GroupDescription], '", "TextColor" : "', [TextColor],'" }' ) AS 'text'
FROM [dbo].[ClassifiedsGroups]
WHERE [GroupStatus] = 'O'