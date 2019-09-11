CREATE PROCEDURE [dbo].[sp_Need_ChangeToActive]
@LastModified DATETIME, @NeedsGUID NCHAR (36)
AS
UPDATE dbo.Need
SET LastModified=@LastModified, State='A', ReminderEmailSent=0, DatePending=null
WHERE NeedsGUID=@NeedsGUID