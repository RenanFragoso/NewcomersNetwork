CREATE PROCEDURE [dbo].[sp_Audit_Log]
@DateCreated DATETIME, @UserName NVARCHAR (256), @ExternalUser NVARCHAR (100), @Message NVARCHAR (256), @Category NVARCHAR (25), @SessionID NVARCHAR (50), @ClientIP NVARCHAR (50)
AS
INSERT INTO dbo.AuditLog (DateCreated, UserName, ExternalUser, Message, Category, SessionID, ClientIP)
VALUES (@DateCreated, @UserName, @ExternalUser, @Message, @Category, @SessionID, @ClientIP);