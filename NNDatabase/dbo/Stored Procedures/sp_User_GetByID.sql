CREATE PROCEDURE [dbo].[sp_User_GetByID]
@UserID INT
AS
SELECT u.ID as 'UserID', *
FROM [User] u
WHERE u.ID = @UserID