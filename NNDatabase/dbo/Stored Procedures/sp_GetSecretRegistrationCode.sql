CREATE PROCEDURE [dbo].[sp_GetSecretRegistrationCode]
WITH EXECUTE AS CALLER
AS
SELECT SecretCode
FROM dbo.SecretRegistrationCode
WHERE Status='O'