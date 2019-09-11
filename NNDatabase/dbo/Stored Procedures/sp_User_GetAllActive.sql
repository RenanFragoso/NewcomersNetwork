CREATE PROCEDURE [dbo].[sp_User_GetAllActive]
AS
SELECT ID as 'UserID', *
FROM [User]
WHERE Status='O'