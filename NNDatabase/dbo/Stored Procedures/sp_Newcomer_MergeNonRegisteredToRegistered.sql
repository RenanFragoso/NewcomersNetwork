CREATE PROCEDURE [dbo].[sp_Newcomer_MergeNonRegisteredToRegistered]
@NonRegisteredUserID INT, @RegisteredUserID INT, @LastModified DATETIME
AS
UPDATE dbo.UserToNeeds
SET UserID=@RegisteredUserID
WHERE UserID=@NonRegisteredUserID

UPDATE [User]
SET LastModified=@LastModified, Status='C'
WHERE ID=@NonRegisteredUserID