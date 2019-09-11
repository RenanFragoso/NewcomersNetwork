CREATE PROCEDURE [dbo].[sp_HostingActivity_RegisterForNewcomer]
@HostingActivityID INT, @UserID INT, @IsSelected BIT=0, @DateCreated DATETIME, @LastModified DATETIME, @State CHAR (1)='N', @DateInProgress DATE=NULL, @DateCompleted DATE=NULL
AS
INSERT INTO dbo.NewcomerToHostingActivities (HostingActivityID, UserID, IsSelected, DateCreated, LastModified, State, DateInProgress, DateCompleted)
VALUES (@HostingActivityID, @UserID, @IsSelected, @DateCreated, @LastModified, @State, @DateInProgress, @DateCompleted)