CREATE PROCEDURE [dbo].[sp_User_CloseRegistration]
@UserID INT, @LastModified DATETIME
AS
BEGIN TRAN

-- 1) Close any relationships if user is a Newcomer
EXECUTE sp_Newcomer_CloseRegistration @UserID, @LastModified
-- 2) Close any relationships if user is a Volunteer

EXECUTE sp_Volunteer_CloseRegistration @UserID, @LastModified
-- 3) Close the User Registration

UPDATE [User]
SET Status='C', LastModified=@LastModified
WHERE ID=@UserID

COMMIT TRAN