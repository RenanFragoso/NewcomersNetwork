CREATE PROCEDURE [dbo].[sp_GetChoices_ServicesGroupsIcon]
WITH EXECUTE AS CALLER
AS
SELECT [GroupId] AS id, CONCAT( '{ "GroupName": "', [GroupName], '", "GroupColor": "', [GroupColor], '", "GroupIcon": "', [GroupIcon], '", "GroupDescription": "', [GroupDescription], '" }' ) AS 'text'
FROM [dbo].[ServicesGroup]
WHERE [GroupStatus] = 'O'