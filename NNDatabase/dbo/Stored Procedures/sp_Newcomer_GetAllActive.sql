CREATE PROCEDURE [dbo].[sp_Newcomer_GetAllActive]
AS
SELECT u.ID as 'UserID', * 
FROM Newcomer 
INNER JOIN [User] u
ON u.ID = Newcomer.UserID
INNER JOIN Mentee
ON u.ID = Mentee.UserID
WHERE Newcomer.Status='O'