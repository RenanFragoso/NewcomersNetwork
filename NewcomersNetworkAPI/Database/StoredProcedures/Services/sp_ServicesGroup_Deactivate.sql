CREATE PROCEDURE [dbo].[sp_ServicesGroup_Deactivate]
@cGroupId nvarchar(128)

AS

UPDATE [dbo].[ServicesGroup]
SET [GroupStatus] = 'C' 
WHERE [GroupId] = @cGroupId