IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Services_GetServicesByGroup]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_Services_GetServicesByGroup]
GO

CREATE PROCEDURE [dbo].[sp_Services_GetServicesByGroup]
	@groupId AS VARCHAR(128)
AS
	SELECT *
	FROM [dbo].[Services]
	WHERE [ServiceGroup] = @groupId
	AND ServiceStatus = 'O'