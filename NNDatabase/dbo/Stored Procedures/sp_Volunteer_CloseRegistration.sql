CREATE PROCEDURE [dbo].[sp_Volunteer_CloseRegistration]
@UserID INT, @LastModified DATETIME
AS
BEGIN TRAN

-- 1) Close relationship between Volunteer to Newcomer
UPDATE dbo.VolunteersToNewcomers
SET Status='C', LastModified=@LastModified
WHERE UserID_Volunteer=@UserID

-- 2) Close the Volunteer Registration
UPDATE dbo.Volunteer
SET Status='C', LastModified=@LastModified
WHERE UserID=@UserID

COMMIT TRAN