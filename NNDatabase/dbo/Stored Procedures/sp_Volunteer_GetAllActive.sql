CREATE PROCEDURE [dbo].[sp_Volunteer_GetAllActive]
AS
SELECT u.ID as 'UserID', * 
FROM Volunteer
INNER JOIN [User] u
ON u.ID = Volunteer.UserID
INNER JOIN Mentor
ON u.ID = Mentor.UserID
WHERE Volunteer.Status='O'