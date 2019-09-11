CREATE PROCEDURE [dbo].[sp_Events_Deactivate]
@cId nvarchar(128)

AS

UPDATE [dbo].[Events]
SET [Status] = 'C' 
WHERE [Id] = @cId