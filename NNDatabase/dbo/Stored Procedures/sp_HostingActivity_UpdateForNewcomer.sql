CREATE PROCEDURE [dbo].[sp_HostingActivity_UpdateForNewcomer]
@HostingActivityID INT, @UserID INT, @IsSelected BIT, @LastModified DATETIME, @State CHAR (1)=NULL, @DateInProgress DATETIME=NULL, @DateCompleted DATETIME=NULL
AS
UPDATE dbo.NewcomerToHostingActivities
SET IsSelected=@IsSelected, LastModified=@LastModified, State=@State, DateInProgress=@DateInProgress, DateCompleted=@DateCompleted
WHERE HostingActivityID=@HostingActivityID AND UserID=@UserID