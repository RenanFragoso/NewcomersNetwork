CREATE PROCEDURE [dbo].[sp_Newcomer_CloseRegistration]
@UserID INT, @LastModified DATETIME
AS
BEGIN TRAN

-- 1) Close relationship between Volunteer to Newcomer
UPDATE dbo.VolunteersToNewcomers
SET Status='C', LastModified=@LastModified
WHERE UserID_Newcomer=@UserID

-- 2) Close any Needs for Newcomer
UPDATE dbo.Need
SET Need.Status='C', Need.LastModified=@LastModified
FROM dbo.Need
JOIN dbo.UserToNeeds
ON Need.ID=UserToNeeds.NeedID
WHERE UserToNeeds.UserID=@UserID

-- 3) Close the Newcomer Registration
UPDATE dbo.Newcomer
SET Status='C', LastModified=@LastModified
WHERE UserID=@UserID

-- Don't need to close the User.

COMMIT TRAN