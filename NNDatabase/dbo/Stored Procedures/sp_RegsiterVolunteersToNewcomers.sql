CREATE PROCEDURE [dbo].[sp_RegsiterVolunteersToNewcomers]
@UserID_Volunteer INT, @UserID_Newcomer INT, @LastModified DATETIME
AS
INSERT INTO dbo.VolunteersToNewcomers (UserID_Volunteer, UserID_Newcomer, Status, LastModified)
VALUES (@UserID_Volunteer, @UserID_Newcomer, 'O', @LastModified)