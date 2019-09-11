CREATE PROCEDURE [dbo].[sp_Need_UpdateReminderEmailSent]
@ID INT, @ReminderEmailSent BIT, @LastModified DATETIME
WITH EXECUTE AS CALLER
AS
UPDATE Need
SET ReminderEmailSent=@ReminderEmailSent, LastModified=@LastModified
WHERE ID=@ID