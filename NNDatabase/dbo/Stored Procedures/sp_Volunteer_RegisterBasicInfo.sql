CREATE PROCEDURE [dbo].[sp_Volunteer_RegisterBasicInfo]
@DateCreated DATE, @AgeRangeID INT=NULL, @HomePhoneNumber BIGINT=NULL, @OtherPhoneNumber BIGINT=NULL, @Gender INT=NULL, @LanguagesSpoken NVARCHAR (100)=NULL, @PostalCode CHAR (7)=NULL, @LastModified DATETIME, @NearestIntersection NVARCHAR (100)=NULL, @HasCar BIT=0, @UserID INT, @ReferenceID INT=NULL
AS
--DECLARE @MyTable table ( ID int )

 INSERT INTO dbo.Volunteer (DateCreated, AgeRangeID, HomePhoneNumber, OtherPhoneNumber, Gender, LanguagesSpoken, Status, PostalCode, LastModified, NearestIntersection, HasCar, UserID, ReferenceID)
 --OUTPUT inserted.ID INTO @MyTable 
 VALUES ( @DateCreated, @AgeRangeID, @HomePhoneNumber, @OtherPhoneNumber, @Gender, @LanguagesSpoken, 'O', @PostalCode, @LastModified, @NearestIntersection, @HasCar, @UserID, @ReferenceID); 
 
 --SELECT ID FROM @MyTable