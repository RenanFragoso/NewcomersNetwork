CREATE PROCEDURE [dbo].[sp_Newcomer_GetByEmail]
@Email NVARCHAR (100)
AS
SELECT u.ID as 'UserID', *
FROM dbo.Newcomer
INNER JOIN [User] u
ON u.ID = Newcomer.UserID
INNER JOIN Mentee m
ON u.ID = m.UserID
WHERE u.Email=@Email AND Newcomer.Status='O'