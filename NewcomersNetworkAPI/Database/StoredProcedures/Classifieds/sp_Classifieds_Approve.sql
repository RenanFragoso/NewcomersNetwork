CREATE PROCEDURE [dbo].[sp_Classifieds_Approve]
@cId nvarchar(128),
@dAlterDate datetime

AS

UPDATE [dbo].[Classifieds] 
SET [Status] = 'O',
[ALterDate] = @dAlterDate
WHERE [Id] = @cId
