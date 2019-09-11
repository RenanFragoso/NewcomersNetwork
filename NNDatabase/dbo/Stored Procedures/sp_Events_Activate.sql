CREATE PROCEDURE [dbo].[sp_Events_Activate]
@cId nvarchar(128)

AS

UPDATE [dbo].[Events]
SET [Status] = 'O' 
WHERE [Id] = @cId