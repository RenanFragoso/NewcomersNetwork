CREATE PROCEDURE [dbo].[sp_GetChoices_EventRegistrationChoices]
@EventID INT
WITH EXECUTE AS CALLER
AS
SELECT *
FROM dbo.Choices_EventRegistrationChoices
WHERE Status='O' AND EventID=@EventID