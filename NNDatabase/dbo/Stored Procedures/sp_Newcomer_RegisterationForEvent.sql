CREATE PROCEDURE [dbo].[sp_Newcomer_RegisterationForEvent]
@UserID INT, @EventID INT, @RegistrationChoiceID INT, @DateCreated DATETIME, @LastModified DATETIME, @IsAttending BIT
AS
INSERT INTO dbo.NewcomerToEventRegistration (UserID, EventID, RegistrationChoiceID, DateCreated, LastModified, IsAttending, Status)
 VALUES (@UserID, @EventID, @RegistrationChoiceID, @DateCreated, @LastModified, @IsAttending, 'O')