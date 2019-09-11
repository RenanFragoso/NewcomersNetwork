IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Events_GetByID]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Events_GetByID]
GO

CREATE PROCEDURE [dbo].[sp_Events_GetByID]
@Id nvarchar(128)
AS
SELECT *
FROM [dbo].[Events]
WHERE [Id] = @Id
AND [Status] = 'O'
AND [Published] = 1