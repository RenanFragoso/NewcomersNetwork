IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Widgets_JustShareCat_Get]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Widgets_JustShareCat_Get]
GO

CREATE PROCEDURE [dbo].[sp_Widgets_JustShareCat_Get]

AS

DECLARE @catId as VARCHAR(128);
DECLARE @catName as VARCHAR(100);

DECLARE @ResTable TABLE (
	CatId VARCHAR(128),
	CatName VARCHAR(128),
	Title VARCHAR(100),
	JsId VARCHAR(128)
);

DECLARE catCur CURSOR FOR 
	SELECT cg.Id, cg.GroupName FROM  ClassifiedsGroups cg
	WHERE cg.GroupStatus = 'O'

OPEN catCur  
FETCH NEXT FROM catCur INTO @catId, @catName

WHILE @@FETCH_STATUS = 0  
BEGIN  
	INSERT INTO @ResTable
		SELECT	TOP 3 
				c.Category AS CatId,
				@catName AS CatName,
				c.Title AS Title,
				c.Id AS JsId
		FROM Classifieds c
		WHERE c.Category = @catId
		AND c.Type = 'S'
		ORDER BY c.CreateDate DESC

    FETCH NEXT FROM catCur INTO @catId, @catName
END 

CLOSE catCur
DEALLOCATE catCur 

SELECT DISTINCT cg.Id, cg.GroupName FROM  ClassifiedsGroups cg WHERE cg.GroupStatus = 'O'
SELECT * FROM @ResTable