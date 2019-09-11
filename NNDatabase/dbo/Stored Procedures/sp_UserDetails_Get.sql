CREATE PROCEDURE [dbo].[sp_UserDetails_Get]
@cUserId nvarchar(128)

AS

SELECT [Id], [FirstName], [LastName], [Title], [Email], [Gender], [MaritalStatus], [AgeRange], [Education], [NearestIntersection], [PostalCode], [ConsentToContact], [Status], [IsImmigrant], [DateCreated], [LastModified]
FROM [dbo].[UserDetails]
WHERE [Id] = @cUserId