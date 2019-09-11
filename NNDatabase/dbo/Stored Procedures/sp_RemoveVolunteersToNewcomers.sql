CREATE PROCEDURE [dbo].[sp_RemoveVolunteersToNewcomers]
@UserID_Volunteer INT, @UserID_Newcomer INT, @LastModified DATETIME
AS
UPDATE dbo.VolunteersToNewcomers
SET Status='C', LastModified=@LastModified
WHERE UserID_Volunteer=@UserID_Volunteer AND UserID_Newcomer=@UserID_Newcomer