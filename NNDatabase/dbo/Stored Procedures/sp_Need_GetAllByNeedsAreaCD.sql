CREATE PROCEDURE [dbo].[sp_Need_GetAllByNeedsAreaCD]
@NeedsAreaCD CHAR (3)
AS
--CREATE TABLE #RESULT(ID INT, Description CHAR(1000), DateCreated DATETIME, DateMet DATETIME, MetByEmail CHAR(100), MetByName CHAR(100), MetByComment CHAR(1000), NeedsCategoryID INT, NeedsAreaCD CHAR(3) , NeedsGUID CHAR(36), State CHAR(1), UserEmail CHAR(100), UserID INT, UserName CHAR(100), UserIsRegistered BIT)

--INSERT INTO #RESULT (ID, Description, DateCreated, DateMet, MetByEmail, MetByName, MetByComment, NeedsCategoryID, NeedsAreaCD, NeedsGUID, State, UserEmail, UserID, UserName, UserIsRegistered)
--SELECT n.ID, n.Description, n.DateCreated, n.DateMet, n.MetByEmail, n.MetByName, n.MetByComment, n.NeedsCategoryID, n.NeedsAreaCD, n.NeedsGUID, n.State, u.Email, u.ID, u.Name, u.IsRegistered
--FROM dbo.Need n
--JOIN dbo.NonRegisteredUserToNeeds u2n
--ON n.ID = u2n.NeedID
--JOIN dbo.NonRegisteredUser u
--ON u2n.UserID = u.ID
--WHERE n.Status='O' AND n.NeedsAreaCD=@NeedsAreaCD

--INSERT INTO #RESULT (ID, Description, DateCreated, DateMet, MetByEmail, MetByName, MetByComment, NeedsCategoryID, NeedsAreaCD, NeedsGUID, State, UserEmail, UserID, UserName, UserIsRegistered)
SELECT n.*, u.Email as 'UserEmail', u.ID as 'UserID', u.Name as 'UserName'
FROM dbo.Need n
JOIN dbo.UserToNeeds u2n
ON n.ID = u2n.NeedID
JOIN [User] u
ON u2n.UserID = u.ID
WHERE n.Status='O' AND n.NeedsAreaCD=@NeedsAreaCD

--SELECT * 
--FROM #RESULT

--DROP TABLE #RESULT