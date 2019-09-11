CREATE PROCEDURE [dbo].[sp_Need_ChangeToMet]
@NeedsGUID NCHAR (36), @LastModified DATETIME
AS
UPDATE dbo.Need
SET LastModified=@LastModified, State='M', DateMet=@LastModified
WHERE NeedsGUID=@NeedsGUID

DECLARE @NeedID INT = 0;
SELECT @NeedID = ID
FROM dbo.Need
WHERE NeedsGUID=@NeedsGUID

DECLARE @USERID INT;
SELECT @USERID = UserID
FROM dbo.UserToNeeds
WHERE UserToNeeds.NeedID=@NeedID

SELECT @USERID as 'UserID'