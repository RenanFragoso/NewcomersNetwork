CREATE PROCEDURE [dbo].[sp_Volunteer_GetByID]
@UserID INT
AS
SELECT u.ID as 'UserID', *
FROM dbo.Volunteer
INNER JOIN [User] u
ON u.ID = Volunteer.UserID
INNER JOIN dbo.Mentor
ON dbo.Volunteer.UserID = dbo.Mentor.UserID
WHERE dbo.Volunteer.UserID = @UserID