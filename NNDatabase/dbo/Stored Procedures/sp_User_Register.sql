CREATE PROCEDURE [dbo].[sp_User_Register]
@Name NVARCHAR (100), @Email NVARCHAR (100), @DateCreated DATETIME, @LastModified DATETIME
AS
INSERT INTO [User] (Name, Email, DateCreated, Status, LastModified)
VALUES (@Name, @Email, @DateCreated, 'O', @LastModified)
SELECT SCOPE_IDENTITY() as 'UserID'