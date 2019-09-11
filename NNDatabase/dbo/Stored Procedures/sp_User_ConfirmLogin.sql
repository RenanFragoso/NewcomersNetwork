CREATE PROCEDURE [dbo].[sp_User_ConfirmLogin]
@cUserName nvarchar(256),
@SecurityStamp nvarchar(max)

AS

DECLARE @cUserId nvarchar(128);
DECLARE @nSTatus nchar(1) = '0';

DECLARE AspUser_Cursor CURSOR FOR
	SELECT [Id]
	FROM [dbo].[AspNetUsers]
	WHERE [UserName] = @cUserName
	AND [SecurityStamp] = @SecurityStamp;

OPEN AspUser_Cursor
FETCH NEXT FROM AspUser_Cursor INTO @cUserId;

IF @@FETCH_STATUS = 0
BEGIN

	DECLARE UserDetails_Cursor CURSOR FOR 
		SELECT [Status] FROM [dbo].[UserDetails] 
		WHERE [Id] = @cUserId
		AND [Status] = 'C'

	OPEN UserDetails_Cursor
	FETCH NEXT FROM UserDetails_Cursor;
	IF @@FETCH_STATUS = 0
	BEGIN
		UPDATE [dbo].[UserDetails]
		SET [Status] = 'O' 
		WHERE CURRENT OF UserDetails_Cursor;
		SET @nStatus = '1';
	END

	CLOSE UserDetails_Cursor;
	DEALLOCATE UserDetails_Cursor;

	IF @nStatus = '1'
	BEGIN
		UPDATE [dbo].[AspNetUsers]
		SET [SecurityStamp] = newid(),
			[EmailConfirmed] = 1
		WHERE CURRENT OF AspUser_Cursor
	END
	
	CLOSE AspUser_Cursor;
	DEALLOCATE AspUser_Cursor;

	SELECT @nStatus
	
END