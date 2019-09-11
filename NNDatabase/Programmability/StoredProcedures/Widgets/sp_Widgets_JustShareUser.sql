IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Widgets_JustShareUser]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Widgets_JustShareUser]
GO

CREATE PROCEDURE [dbo].[sp_Widgets_JustShareUser]
@userId nvarchar(128),
@classifieds int = 10,
@page int = 1

AS

DECLARE @Offset INT = (@page - 1) * @classifieds;

SELECT *
FROM CLassifieds
WHERE CreatedBy = @userId
AND Status IN ('O', 'P')
ORDER BY CreateDate DESC
OFFSET @Offset ROWS
FETCH NEXT @classifieds ROWS ONLY

SELECT COUNT(*) AS TotalJs
FROM CLassifieds
WHERE CreatedBy = @userId
AND Status IN ('O', 'P')