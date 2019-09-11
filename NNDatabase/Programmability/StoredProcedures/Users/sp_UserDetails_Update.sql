
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserDetails_Update]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_UserDetails_Update]
GO

CREATE PROCEDURE [dbo].[sp_UserDetails_Update]
@Id nvarchar(128),
@FirstName nvarchar(100),
@LastName nvarchar(100),
@Email nvarchar(100),
@Gender nvarchar(1),
@MaritalStatus nvarchar(2),
@AgeRange nvarchar(2),
@Education nvarchar(2),
@NearestIntersection nvarchar(256),
@PostalCode nvarchar(7),
@ConsentToContact bit,
@IsImmigrant bit,
@Title nvarchar(50)=''

AS

UPDATE [dbo].[UserDetails] 
SET [FirstName] = @FirstName, 
	[LastName] = @LastName, 
	[Email] = @Email,
	[Title] = @Title,
	[Gender] = @Gender,
	[MaritalStatus] = @MaritalStatus, 
	[AgeRange] = @AgeRange, 
	[Education] = @Education, 
	[NearestIntersection] = @NearestIntersection, 
	[PostalCode] = @PostalCode, 
	[ConsentToContact] = @ConsentToContact,
	[IsImmigrant] = @IsImmigrant,
	[LastModified] = GETDATE()
WHERE Id = @Id