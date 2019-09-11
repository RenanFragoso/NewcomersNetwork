CREATE PROCEDURE [dbo].[sp_ServicesGroup_Delete]
@cGroupId nvarchar(128)

AS

DELETE FROM [dbo].[ServicesGroup]
WHERE [GroupId] = @cGroupId