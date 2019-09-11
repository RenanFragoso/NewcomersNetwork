CREATE PROCEDURE [dbo].[sp_Newcomer_GetByID]
@UserID INT
AS
SELECT u.ID as 'UserID', *
FROM dbo.Newcomer
INNER JOIN [User] u
ON u.ID = dbo.Newcomer.UserID
INNER JOIN dbo.Mentee
ON dbo.Mentee.UserID = u.ID
WHERE dbo.Newcomer.UserID = @UserID