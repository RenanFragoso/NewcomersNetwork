CREATE PROCEDURE [dbo].[sp_Mentee_GetLatestForMentor]
@UserID_Volunteer INT
AS
SELECT TOP 1 *
FROM dbo.VolunteersToNewcomers
WHERE UserID_Volunteer=@UserID_Volunteer
ORDER BY LastModified DESC