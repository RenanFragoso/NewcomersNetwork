CREATE PROCEDURE [dbo].[sp_Events_GetByID]
@cId nvarchar(128)
AS
SELECT *
FROM [dbo].[Events]
WHERE [Id] = @cId