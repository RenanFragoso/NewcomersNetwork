IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Widgets_Banners_GetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Widgets_Banners_GetById]
GO

CREATE PROCEDURE [dbo].[sp_Widgets_Banners_GetById]
@id nvarchar(128)

AS

SELECT [Id], [Link], [Title1], [Title2], [Status]
FROM [dbo].[Widgets_Banners] 
WHERE [Id] = @id