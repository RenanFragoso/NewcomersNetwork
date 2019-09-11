CREATE PROCEDURE [dbo].[sp_Classifieds_Update]
@cId nvarchar(128),
@cTitle nvarchar(100),
@cText ntext = '', 
@cCreatedBy nvarchar(128),
@cCategory nvarchar(128),
@cType nchar(1) = '',
@cImage nvarchar(256),
@dAlterDate datetime

AS

UPDATE [dbo].[Classifieds] 
SET [Title] = @cTitle, 
[Text] = @cText, 
[CreatedBy] = @cCreatedBy,
[Category] = @cCategory,
[Type] = @cType,
[Image] = @cImage,
[AlterDate] = @dAlterDate
WHERE [Id] = @cId