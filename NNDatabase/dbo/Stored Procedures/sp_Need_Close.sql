CREATE PROCEDURE [dbo].[sp_Need_Close]
@ID INT, @LastModified DATETIME
AS
UPDATE dbo.Need
SET Status='C', LastModified=@LastModified
WHERE ID=@ID

DECLARE @USERID INT;
SELECT @USERID = UserID
FROM dbo.UserToNeeds
WHERE UserToNeeds.NeedID=@ID

SELECT @USERID as 'UserID'