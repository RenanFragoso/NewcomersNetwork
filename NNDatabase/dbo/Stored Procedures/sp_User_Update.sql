CREATE PROCEDURE [dbo].[sp_User_Update]
@Name NVARCHAR (100), @Email NVARCHAR (100), @LastModified DATETIME, @UserID INT
WITH EXECUTE AS CALLER
AS
UPDATE [User]
SET Name=@Name, Email=@Email, LastModified=@LastModified
WHERE ID=@UserID