CREATE PROCEDURE [dbo].[sp_GetChoices_ClassifiedGroupsFull]

AS

SELECT [Id] AS 'id', CONCAT( '{ "Id": "', [Id], '", "GroupName": "', [GroupName], '", "GroupColor": "', [GroupColor], '", "GroupIcon": "', [GroupIcon], '", "GroupDescription": "', [GroupDescription], '" }' ) AS 'text'
FROM [dbo].[ClassifiedsGroups]
WHERE [GroupStatus] = 'O'