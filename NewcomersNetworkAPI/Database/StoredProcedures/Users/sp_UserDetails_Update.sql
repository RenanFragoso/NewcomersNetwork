CREATE PROCEDURE [dbo].[sp_UserDetails_Update]
@cId nvarchar(128),
@cFirstName nvarchar(100),
@cLastName nvarchar(100),
@cEmail nvarchar(100),
@cGender nvarchar(1),
@cMaritalStatus nvarchar(2),
@cAgeRange nvarchar(2),
@cEducation nvarchar(2),
@cNearestIntersection nvarchar(256),
@cPostalCode nvarchar(7),
@bConsentToContact bit,
@bIsImmigrant bit,
@cTitle nvarchar(50)='',
@dModify datetime

AS

UPDATE [dbo].[UserDetails] 
SET [FirstName] = @cFirstName, 
	[LastName] = @cLastName, 
	[Email] = @cEmail,
	[Title] = @cTitle,
	[Gender] = @cGender,
	[MaritalStatus] = @cMaritalStatus, 
	[AgeRange] = @cAgeRange, 
	[Education] = @cEducation, 
	[NearestIntersection] = @cNearestIntersection, 
	[PostalCode] = @cPostalCode, 
	[ConsentToContact] = @bConsentToContact,
	[IsImmigrant] = @bIsImmigrant,
	[LastModified] = @dModify
WHERE Id = @cId