CREATE PROCEDURE [dbo].[sp_ServicesGroup_Activate]
@cGroupId nvarchar(128)

AS

UPDATE [dbo].[ServicesGroup]
SET [GroupStatus] = 'O' 
WHERE [GroupId] = @cGroupId