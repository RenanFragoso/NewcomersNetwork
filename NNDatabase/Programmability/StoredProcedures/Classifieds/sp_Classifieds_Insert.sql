IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Classifieds_Insert]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Classifieds_Insert]
GO

CREATE PROCEDURE [dbo].[sp_Classifieds_Insert]
@Id nvarchar(128),
@cTitle nvarchar(100),
@cText ntext = '', 
@cCreatedBy nvarchar(128),
@cCategory nvarchar(128),
@cType nchar(1) = '',
@cImage nvarchar(256),
@dCreateDate datetime

AS

DECLARE @NEWIDTBL Table (
	Id nvarchar(128)
);

DECLARE @NewId nvarchar(128);

-- Creates a blocked classified
IF @Id = '-1' OR @Id = ''
	BEGIN
		INSERT INTO [dbo].[Classifieds] 
		( [Title], [Text], [CreatedBy], [Category], [Type], [Image], [Status], [CreateDate], [AlterDate] )
		OUTPUT Inserted.Id into @NEWIDTBL
		VALUES ( @cTitle, @cText, @cCreatedBy, @cCategory, @cType, @cImage, 'P', @dCreateDate, @dCreateDate )
		
		SELECT @NewId = Id from @NEWIDTBL;

		UPDATE [dbo].[Classifieds]
		SET [Image] = @NewId
		WHERE [Id] = @NewId
		
		SELECT Id AS LAST_CLASSIFIED FROM @NEWIDTBL
	END
ELSE
	BEGIN
		INSERT INTO [dbo].[Classifieds] 
		( [Id], [Title], [Text], [CreatedBy], [Category], [Type], [Image], [Status], [CreateDate], [AlterDate] )
		OUTPUT Inserted.Id into @NEWIDTBL
		VALUES ( @Id, @cTitle, @cText, @cCreatedBy, @cCategory, @cType, @cImage, 'P', @dCreateDate, @dCreateDate )
		SELECT @Id AS LAST_CLASSIFIED
	END

GO