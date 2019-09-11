CREATE PROCEDURE [dbo].[sp_Mentor_GetLatestForMentee]
@UserID_Newcomer INT
AS
SELECT TOP 1 *
FROM dbo.VolunteersToNewcomers
WHERE UserID_Newcomer=@UserID_Newcomer
ORDER BY LastModified DESC