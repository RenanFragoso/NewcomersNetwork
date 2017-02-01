CREATE PROCEDURE [dbo].[sp_ServicesGroup_Insert]
@cName nvarchar(100),
@cDescription nvarchar(256) = '',
@cColor nvarchar(7),
@cIcon nvarchar(256) = '',
@dCreateDate datetime,
@dAlterDate datetime

AS

DECLARE @NEWIDTBL Table (
	Id nvarchar(128)
);

INSERT INTO [dbo].[ServicesGroup]
([GroupName], [GroupDescription], [GroupColor], [GroupIcon], [CreateDate], [AlterDate])
OUTPUT Inserted.GroupId into @NEWIDTBL
VALUES (@cName, @cDescription, @cColor, @cIcon, @dCreateDate, @dAlterDate)

SELECT Id AS LAST_SERVICEGROUP FROM @NEWIDTBL