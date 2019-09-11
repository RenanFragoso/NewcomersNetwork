CREATE PROCEDURE [dbo].[sp_Newcomer_UpdateBasicInfo]
@AgeRangeID INT=NULL, @HomePhoneNumber BIGINT=NULL, @OtherPhoneNumber BIGINT=NULL, @Gender INT=NULL, @LengthOfTimeInCAD INT=NULL, @CountryOfOrigin NVARCHAR (50)=NULL, @ImmigrantCategory INT=NULL, @LanguagesSpoken NVARCHAR (100)=NULL, @UserID INT, @LastModified DATETIME, @LevelOfEnglish INT=NULL, @PostalCode CHAR (7)=NULL, @NearestIntersection NVARCHAR (100)=NULL, @MaritalStatus INT=NULL, @Education INT=NULL, @OtherTraining NVARCHAR (2000)=NULL
AS
UPDATE n
SET n.AgeRangeID=@AgeRangeID, n.HomePhoneNumber=@HomePhoneNumber, n.OtherPhoneNumber=@OtherPhoneNumber, n.Gender=@Gender, n.LengthOfTimeInCAD=@LengthOfTimeInCAD, n.CountryOfOrigin=@CountryOfOrigin, n.ImmigrantCategory=@ImmigrantCategory, n.LanguagesSpoken=@LanguagesSpoken, n.LastModified=@LastModified, n.LevelOfEnglish=@LevelOfEnglish, n.PostalCode=@PostalCode, n.NearestIntersection=@NearestIntersection, n.MaritalStatus=@MaritalStatus, n.Education=@Education, n.OtherTraining=@OtherTraining
FROM dbo.Newcomer n
INNER JOIN [User] u
ON u.ID=n.UserID
WHERE u.ID=@UserID