CREATE PROCEDURE [dbo].[sp_ServicesGroup_Update]
@cId nvarchar(128),
@cName nvarchar(100),
@cDescription nvarchar(256) = '',
@cColor nvarchar(7),
@cIcon nvarchar(256) = '',
@dAlterDate datetime

AS

UPDATE [dbo].[ServicesGroup]
SET [GroupName] = @cName,
	[GroupDescription] = @cDescription,
	[GroupColor] = @cColor,
	[GroupIcon] = @cIcon,
	[AlterDate] = @dAlterDate
WHERE [GroupId] = @cId