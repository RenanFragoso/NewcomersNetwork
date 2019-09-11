CREATE PROCEDURE [dbo].[sp_HostingActivity_GetForNewcomer]
@UserID INT
AS
SELECT *
FROM dbo.NewcomerToHostingActivities
JOIN dbo.Choices_HostingActivities
ON NewcomerToHostingActivities.HostingActivityID=Choices_HostingActivities.ID
WHERE UserID=@UserID