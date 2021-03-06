CREATE PROCEDURE [dbo].[sp_Classifieds_GetFiltered]
@nPage int=1,
@nPageSize int=10,
@cCategory nvarchar(max) ='',
@cType nchar(1)='',
@cWord nvarchar(100)='' 

AS

DECLARE @SQLStatement nvarchar(max) = '';
DECLARE @nCurPage int = (@nPage - 1) * @nPageSize;

DECLARE @Type nchar(1)='';
DECLARE @Category nvarchar(max) = N'';
DECLARE @Words nvarchar(100) = N'';

SET @Type = REPLACE(@cType, '''', '');
SET @Category = REPLACE(@cCategory, '''', '''''');
SET @Words = REPLACE(@cWord, '''', '''''');

SET @SQLStatement = N'SELECT * FROM [dbo].[Classifieds] WHERE [Status] = ''O''';

IF (LEN(ISNULL(@Category,'')) > 0)
BEGIN
	SET @SQLStatement += N' AND ' + QUOTENAME(@Category,'''') + N' LIKE (''%'' + [Category] +''%'')';
END;

IF (LEN(ISNULL(@Type,'')) > 0)
BEGIN
	SET @SQLStatement += N' AND [Type] = ' + QUOTENAME(@Type,'''');
END;

IF (LEN(ISNULL(@Words,'')) > 0)
BEGIN
	SET @SQLStatement += N' AND ( ([Title] LIKE ' + QUOTENAME('%' + @Words + '%','''') + N') OR ([Text] LIKE ' + QUOTENAME('%' + @Words + '%','''') + N') )';
END;

SET @SQLStatement += N' ORDER BY [CreateDate] OFFSET ' + CONVERT(nvarchar, @nCurPage) + N' ROWS FETCH NEXT ' + CONVERT(nvarchar, @nPageSize) + N' ROWS ONLY';

--PRINT @SQLStatement;

EXEC(@SQLStatement);
