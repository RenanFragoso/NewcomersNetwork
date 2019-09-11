CREATE PROCEDURE [dbo].[sp_Need_GetReminderEmails]
@MaxDaysUntilReminder INT
AS
SELECT n.*, u.Email as 'UserEmail', u.ID as 'UserID', u.Name as 'UserName'
FROM dbo.Need n
JOIN dbo.UserToNeeds
ON dbo.UserToNeeds.NeedID = n.ID
JOIN [User] u
ON u.ID = dbo.UserToNeeds.UserID
WHERE n.ReminderEmailSent = 0 AND DATEDIFF(day, n.DatePending, GETDATE()) > @MaxDaysUntilReminder