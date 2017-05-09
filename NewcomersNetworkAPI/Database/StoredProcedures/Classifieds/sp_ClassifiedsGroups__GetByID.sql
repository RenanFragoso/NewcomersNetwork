CREATE PROCEDURE [dbo].[sp_ClassifiedsGroups__GetByID]
@cId nvarchar(128)
AS
SELECT *
FROM [dbo].[ClassifiedsGroups]
WHERE [Id] = @cId