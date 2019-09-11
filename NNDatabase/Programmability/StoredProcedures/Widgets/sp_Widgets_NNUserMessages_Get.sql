IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Widgets_NNUserMessages_Get]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Widgets_NNUserMessages_Get]
GO

CREATE PROCEDURE [dbo].[sp_Widgets_NNUserMessages_Get]

AS

SELECT [Position], [BlockColor], [Text]
FROM [dbo].[Widgets_ServiceBlocks] 
WHERE [Status] = 'O'
ORDER BY [Position]