CREATE PROCEDURE [dbo].[sp_Newcomer_RegisterBasicInfo]
@DateCreated DATE, @AgeRangeID INT=NULL, @HomePhoneNumber BIGINT=NULL, @OtherPhoneNumber BIGINT=NULL, @Gender INT=NULL, @LengthOfTimeInCAD INT=NULL, @CountryOfOrigin NVARCHAR (50)=NULL, @ImmigrantCategory INT=NULL, @LanguagesSpoken NVARCHAR (100)=NULL, @LastModified DATETIME, @DateOfRegistration DATE=NULL, @LevelOfEnglish INT=NULL, @PostalCode CHAR (7)=NULL, @NearestIntersection NVARCHAR (100)=NULL, @UserID INT, @MaritalStatus INT=NULL, @Education INT=NULL, @OtherTraining NVARCHAR (2000)=NULL
AS
--DECLARE @MyTable table ( ID int )

 INSERT INTO dbo.Newcomer (DateCreated, AgeRangeID, HomePhoneNumber, OtherPhoneNumber, Gender, LengthOfTimeInCAD, CountryOfOrigin, LanguagesSpoken, Status, LastModified, LevelOfEnglish, PostalCode, NearestIntersection, UserID, MaritalStatus, Education, OtherTraining)
 --OUTPUT inserted.ID INTO @MyTable 
 VALUES (@DateCreated, @AgeRangeID, @HomePhoneNumber, @OtherPhoneNumber, @Gender, @LengthOfTimeInCAD, @CountryOfOrigin, @LanguagesSpoken, 'O', @LastModified, @LevelOfEnglish, @PostalCode, @NearestIntersection, @UserID, @MaritalStatus, @Education, @OtherTraining); 
 
 --SELECT ID FROM @MyTable