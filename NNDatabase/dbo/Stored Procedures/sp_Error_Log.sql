CREATE PROCEDURE [dbo].[sp_Error_Log]
@DateCreated DATETIME, @Message NVARCHAR (MAX), @Context NVARCHAR (512)=NULL
WITH EXECUTE AS CALLER
AS
INSERT INTO dbo.ErrorLog (DateCreated, Message, Context)
VALUES (@DateCreated, @Message, @Context);