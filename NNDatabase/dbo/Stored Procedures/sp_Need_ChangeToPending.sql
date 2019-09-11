CREATE PROCEDURE [dbo].[sp_Need_ChangeToPending]
@LastModified DATETIME, @NeedsGUID NCHAR (36), @DatePending DATETIME, @MetByName NVARCHAR (100), @MetByEmail NVARCHAR (100)
AS
UPDATE dbo.Need
SET LastModified=@LastModified, State='P', DatePending=@DatePending, MetByName=@MetByName, MetByEmail=@MetByEmail
WHERE NeedsGUID=@NeedsGUID