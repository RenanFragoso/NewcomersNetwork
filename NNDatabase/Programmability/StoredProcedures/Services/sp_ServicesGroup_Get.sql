IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ServicesGroup_Get]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_ServicesGroup_Get]
GO

CREATE PROCEDURE [dbo].[sp_ServicesGroup_Get]
AS
SELECT *
FROM [dbo].[ServicesGroup]
WHERE [GroupStatus] = 'O'
AND [Hidden] = 'N'