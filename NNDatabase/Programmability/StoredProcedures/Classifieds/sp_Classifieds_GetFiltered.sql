IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Classifieds_GetFiltered]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Classifieds_GetFiltered]
GO

CREATE PROCEDURE [dbo].[sp_Classifieds_GetFiltered]
@Page int=1,
@PageSize int=10,
@Category nvarchar(max) =N'',
@Type nchar(1)=N'',
@Word nvarchar(100)=N'' 

AS

DECLARE @SQLStatement nvarchar(max) = N'';
DECLARE @CurPage int = (@Page - 1) * @PageSize;

--DECLARE @Type nchar(1)='';
--DECLARE @Category nvarchar(max) = N'';
DECLARE @Words nvarchar(100) = N'';

SET @Type = REPLACE(@Type, '''', '');
SET @Category = REPLACE(@Category, '''', '''''');
SET @Words = REPLACE(@Word, '''', '''''');

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

SET @SQLStatement += N' ORDER BY [CreateDate] OFFSET ' + CONVERT(nvarchar, @CurPage) + N' ROWS FETCH NEXT ' + CONVERT(nvarchar, @PageSize) + N' ROWS ONLY';

--PRINT @SQLStatement;

EXEC(@SQLStatement);
