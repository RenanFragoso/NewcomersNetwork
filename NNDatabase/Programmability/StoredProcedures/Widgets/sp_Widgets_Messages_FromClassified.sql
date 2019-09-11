IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Widgets_Messages_FromClassified]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Widgets_Messages_FromClassified]
GO

CREATE PROCEDURE [dbo].[sp_Widgets_Messages_FromClassified]
@classifiedId nvarchar(128)

AS

SELECT *
FROM [dbo].[ClassifiedsMessages]
WHERE [ClassifiedId] = @classifiedId
AND [Status] = 'O'
ORDER BY [Date], [Id], [ReplyTo]