CREATE PROCEDURE [dbo].[sp_GetChoices_NeedsCategory]
@NeedsAreaCD CHAR (3)
AS
IF @NeedsAreaCD = 'ALL' BEGIN
  SELECT * FROM Choices_NeedsCategory
  WHERE Status = 'O'
  ORDER BY Description ASC
END
ELSE BEGIN
  SELECT * FROM Choices_NeedsCategory
  WHERE Status = 'O' AND AreaCD=@NeedsAreaCD
  ORDER BY Description ASC
END