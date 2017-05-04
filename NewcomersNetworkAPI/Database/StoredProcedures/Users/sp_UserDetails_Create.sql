CREATE PROCEDURE [dbo].[sp_UserDetails_Create]
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
@cStatus char(1),
@cTitle nvarchar(50)='',
@dCreate datetime,
@dModify datetime

AS

INSERT INTO [dbo].[UserDetails] 
([Id], [FirstName], [LastName], [Email], [Gender], [MaritalStatus], [AgeRange], [Education], [NearestIntersection], [PostalCode], [ConsentToContact], [IsImmigrant], [Status], [Title], [DateCreated], [LastModified])
VALUES (@cId, @cFirstName, @cLastName, @cEmail, @cGender, @cMaritalStatus, @cAgeRange, @cEducation, @cNearestIntersection, @cPostalCode, @bConsentToContact, @bIsImmigrant, @cStatus, @cTitle, @dCreate, @dModify)