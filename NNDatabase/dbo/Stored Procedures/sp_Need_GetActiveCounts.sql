CREATE PROCEDURE [dbo].[sp_Need_GetActiveCounts]
AS
SELECT COUNT(Need.ID) AS 'SET'
FROM dbo.Need
JOIN dbo.Choices_NeedsCategory
ON Need.NeedsCategoryID = Choices_NeedsCategory.ID
JOIN dbo.UserToNeeds
ON Need.ID = UserToNeeds.NeedID
WHERE Need.Status='O' and Need.State='A' and Choices_NeedsCategory.AreaCD='SET'

SELECT COUNT(Need.ID) AS 'EMP'
FROM dbo.Need
JOIN dbo.Choices_NeedsCategory
ON Need.NeedsCategoryID = Choices_NeedsCategory.ID
JOIN dbo.UserToNeeds
ON Need.ID = UserToNeeds.NeedID
WHERE Need.Status='O' and Need.State='A' and Choices_NeedsCategory.AreaCD='EMP'

SELECT COUNT(Need.ID) AS 'HOU'
FROM dbo.Need
JOIN dbo.Choices_NeedsCategory
ON Need.NeedsCategoryID = Choices_NeedsCategory.ID
JOIN dbo.UserToNeeds
ON Need.ID = UserToNeeds.NeedID
WHERE Need.Status='O' and Need.State='A' and Choices_NeedsCategory.AreaCD='HOU'