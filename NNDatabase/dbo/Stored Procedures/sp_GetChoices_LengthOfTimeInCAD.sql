CREATE PROCEDURE [dbo].[sp_GetChoices_LengthOfTimeInCAD]
AS
SELECT * FROM Choices_LengthOfTimeInCAD
WHERE Status = 'O'