CREATE PROCEDURE [dbo].[sp_Classifieds_Insert]
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

-- Creates a blocked classified
INSERT INTO [dbo].[Classifieds] 
( [Title], [Text], [CreatedBy], [Category], [Type], [Image], [Status], [CreateDate], [AlterDate] )
OUTPUT Inserted.Id into @NEWIDTBL
VALUES ( @cTitle, @cText, @cCreatedBy, @cCategory, @cType, @cImage, 'P', @dCreateDate, @dCreateDate )

SELECT Id AS LAST_CLASSIFIED FROM @NEWIDTBL