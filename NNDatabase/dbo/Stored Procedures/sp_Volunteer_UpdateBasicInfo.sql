CREATE PROCEDURE [dbo].[sp_Volunteer_UpdateBasicInfo]
@AgeRangeID INT=NULL, @HomePhoneNumber BIGINT=NULL, @OtherPhoneNumber BIGINT=NULL, @Gender MONEY=NULL, @LanguagesSpoken NVARCHAR (100)=NULL, @PostalCode NVARCHAR (10)=NULL, @LastModified DATETIME=NULL, @UserID INT, @NearestIntersection NVARCHAR (100)=NULL, @HasCar BIT=0, @ReferenceID INT=NULL
AS
UPDATE dbo.Volunteer
SET AgeRangeID=@AgeRangeID, HomePhoneNumber=@HomePhoneNumber, OtherPhoneNumber=@OtherPhoneNumber, Gender=@Gender, LanguagesSpoken=@LanguagesSpoken, PostalCode=@PostalCode, LastModified=@LastModified, NearestIntersection=@NearestIntersection, HasCar=@HasCar, ReferenceID=@ReferenceID
WHERE UserID=@UserID