IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Message_FromClassified]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Message_FromClassified]
GO

CREATE PROCEDURE [dbo].[sp_Message_FromClassified]
@classifiedId nvarchar(128)

AS

SELECT -1 as Id, [Id] as [ClassifiedId], '' as [From], [CreatedBy] as [To], SYSDATETIME() as [Date], '' as [Message], SYSDATETIME() as [AlterDate], 'A' as [Status], '' as [ReplyTo]
FROM [dbo].[Classifieds]
WHERE [Id] = @classifiedId