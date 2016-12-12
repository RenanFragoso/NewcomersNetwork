CREATE PROCEDURE [dbo].[sp_GetChoices_HostingActivities]
WITH EXECUTE AS CALLER
AS
SELECT *
FROM dbo.Choices_HostingActivities
WHERE Status='O'