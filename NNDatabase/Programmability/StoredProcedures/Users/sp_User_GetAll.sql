CREATE PROCEDURE [dbo].[sp_User_GetAll]

AS

SELECT [Id], [Email], [UserName], [SecurityStamp]
FROM [dbo].[AspNetUsers]
