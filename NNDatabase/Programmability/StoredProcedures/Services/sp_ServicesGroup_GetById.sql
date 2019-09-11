CREATE PROCEDURE [dbo].[sp_ServicesGroup_GetById]
@cGroupId nvarchar(128)

AS

SELECT *
FROM [dbo].[ServicesGroup]
WHERE [GroupId] = @cGroupId