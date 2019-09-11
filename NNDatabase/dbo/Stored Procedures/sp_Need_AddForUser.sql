CREATE PROCEDURE [dbo].[sp_Need_AddForUser]
@UserID INT, @Description NVARCHAR (1000), @DateCreated DATETIME, @LastModified DATETIME, @NeedsCategoryID INT, @NeedsGUID CHAR (36), @NeedsAreaCD CHAR (3)
AS
BEGIN TRAN
DECLARE @NewNeedID INT;

INSERT INTO dbo.Need (Description, Status, State, DateCreated, LastModified, NeedsCategoryID, NeedsGUID, NeedsAreaCD)
VALUES (@Description, 'O', 'A', @DateCreated, @LastModified, @NeedsCategoryID, @NeedsGUID, @NeedsAreaCD)
SELECT @NewNeedID = SCOPE_IDENTITY()

INSERT INTO dbo.UserToNeeds (NeedID, UserID, DateCreated, LastModified)
VALUES (@NewNeedID, @UserID, @DateCreated, @LastModified)
COMMIT TRAN