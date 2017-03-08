CREATE PROCEDURE [dbo].[sp_Classifieds_Reject]
@cId nvarchar(128),
@dAlterDate datetime

AS

UPDATE [dbo].[Classifieds] 
SET [Status] = 'R',
[ALterDate] = @dAlterDate
WHERE [Id] = @cId
