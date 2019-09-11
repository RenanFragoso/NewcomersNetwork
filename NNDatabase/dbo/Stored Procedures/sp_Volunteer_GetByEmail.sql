CREATE PROCEDURE [dbo].[sp_Volunteer_GetByEmail]
@Email NVARCHAR (100)
WITH EXECUTE AS CALLER
AS
SELECT u.ID as 'UserID', *
FROM dbo.Volunteer
INNER JOIN [User] u
ON u.ID = Volunteer.UserID
INNER JOIN Mentor m
ON u.ID = m.UserID
WHERE u.Email=@Email AND Volunteer.Status='O'