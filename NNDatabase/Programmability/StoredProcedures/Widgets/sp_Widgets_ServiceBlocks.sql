IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Widgets_ServiceBlocks]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Widgets_ServiceBlocks]
GO

CREATE PROCEDURE [dbo].[sp_Widgets_ServiceBlocks]

AS

SELECT [Position], [ServiceGroupId], [Icon], [BlockColor], [Text]
FROM [dbo].[Widgets_ServiceBlocks] 
WHERE [Status] = 'O'
ORDER BY [Position]