CREATE PROCEDURE [dbo].[sp_ServicesGroup_GetById]
@cGroupId nvarchar(126)

AS

SELECT *
FROM [dbo].[ServicesGroup]
WHERE [GroupId] = @cGroupId