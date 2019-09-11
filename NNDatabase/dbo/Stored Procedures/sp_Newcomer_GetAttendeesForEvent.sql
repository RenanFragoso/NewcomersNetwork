CREATE PROCEDURE [dbo].[sp_Newcomer_GetAttendeesForEvent]
@EventID INT
AS
SELECT * 
FROM NewcomerToEventRegistration nter
INNER JOIN Newcomer n
ON n.UserID = nter.UserID
INNER JOIN [User] u
ON u.ID = n.UserID
INNER JOIN Mentee m
ON m.UserID = n.UserID
WHERE nter.EventID=@EventID AND nter.Status='O'