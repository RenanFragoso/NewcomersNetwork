CREATE PROCEDURE [dbo].[sp_Mentee_GetAllForMentor]
@UserID_Volunteer INT
AS
-- Get all the current Mentees for the given Mentor
SELECT u.ID as 'UserID_Newcomer', u.Name as 'UserName_Newcomer', dbo.Mentee.Profession as 'Profession', dbo.Mentee.Specialization as 'Specialization'
FROM dbo.VolunteersToNewcomers
INNER JOIN [User] u
ON u.ID = @UserID_Volunteer
INNER JOIN dbo.Newcomer
ON dbo.Newcomer.UserID = u.ID
INNER JOIN dbo.Mentee
ON dbo.Mentee.UserID = dbo.Newcomer.UserID
WHERE dbo.VolunteersToNewcomers.Status = 'O' AND dbo.Newcomer.Status ='O' AND dbo.VolunteersToNewcomers.UserID_Volunteer = @UserID_Volunteer