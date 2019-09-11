IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Classifieds_Finish]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Classifieds_Finish]
GO

CREATE PROCEDURE [dbo].[sp_Classifieds_Finish]
@Id nvarchar(128)

AS

UPDATE [dbo].[Classifieds] 
SET [Status] = 'F'
WHERE [Id] = @Id