CREATE PROCEDURE [dbo].[sp_Need_GetForUser]
@UserID INT
AS
SELECT Need.*
FROM dbo.UserToNeeds
JOIN dbo.Need
ON dbo.UserToNeeds.NeedID = dbo.Need.ID
WHERE Status='O' AND dbo.UserToNeeds.UserID = @UserID