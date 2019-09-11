CREATE PROCEDURE [dbo].[sp_Need_GetByID]
@ID INT
AS
SELECT u.Email as UserEmail, u.Name as UserName, *
FROM dbo.Need
JOIN dbo.UserToNeeds
ON dbo.UserToNeeds.NeedID = dbo.Need.ID
JOIN [User] u
ON u.ID = dbo.UserToNeeds.UserID
WHERE dbo.Need.ID = @ID