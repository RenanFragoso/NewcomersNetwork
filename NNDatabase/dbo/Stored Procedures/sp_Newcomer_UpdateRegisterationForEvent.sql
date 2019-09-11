CREATE PROCEDURE [dbo].[sp_Newcomer_UpdateRegisterationForEvent]
@UserID INT, @EventID INT, @RegistrationChoiceID INT, @LastModified DATETIME, @IsAttending BIT
AS
UPDATE dbo.NewcomerToEventRegistration
SET IsAttending=@IsAttending, LastModified=@LastModified
WHERE UserID=@UserID AND EventID=@EventID AND RegistrationChoiceID=@RegistrationChoiceID