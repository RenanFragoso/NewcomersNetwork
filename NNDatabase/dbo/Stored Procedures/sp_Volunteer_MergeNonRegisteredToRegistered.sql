CREATE PROCEDURE [dbo].[sp_Volunteer_MergeNonRegisteredToRegistered]
@RegisteredUserID INT, @NonRegisteredUserID INT, @LastModified DATETIME
WITH EXECUTE AS CALLER
AS
UPDATE dbo.UserToNeeds
SET UserID=@RegisteredUserID
WHERE UserID=@NonRegisteredUserID

UPDATE [User]
SET LastModified=@LastModified, Status='C'
WHERE ID=@NonRegisteredUserID