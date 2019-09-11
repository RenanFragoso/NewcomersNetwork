﻿CREATE PROCEDURE [dbo].[sp_Mentee_RegisterBasicInfo]
@UserID INT, @LevelOfEnglish INT=NULL, @LevelOfEducation NVARCHAR (200)=NULL, @HaveCredentialsAssessedInCAD BIT=NULL, @CredentialsAssessedInCAD_Where NVARCHAR (50)=NULL, @IsCurrentlyWorking BIT=NULL, @CurrentlyWorking_HoursPerWeek INT=NULL, @HavePreviouslyWorkedInFieldInCAD BIT=NULL, @PastWorkExperience NVARCHAR (1000)=NULL, @HaveCompletedJobPrepProgram BIT=NULL, @CompletedJobPrepProgram_Where NVARCHAR (50)=NULL, @Profession NVARCHAR (100)=NULL, @Specialization NVARCHAR (100)=NULL, @OccupationID INT=NULL
AS
INSERT INTO dbo.Mentee (UserID, LevelOfEducation, HaveCredentialsAssessedInCAD, CredentialsAssessedInCAD_Where, IsCurrentlyWorking, CurrentlyWorking_HoursPerWeek, HavePreviouslyWorkedInFieldInCAD, PastWorkExperience, HaveCompletedJobPrepProgram, CompletedJobPrepProgram_Where, Profession, Specialization, OccupationID)
 VALUES (@UserID, @LevelOfEducation, @HaveCredentialsAssessedInCAD, @CredentialsAssessedInCAD_Where, @IsCurrentlyWorking, @CurrentlyWorking_HoursPerWeek, @HavePreviouslyWorkedInFieldInCAD, @PastWorkExperience, @HaveCompletedJobPrepProgram, @CompletedJobPrepProgram_Where, @Profession, @Specialization, @OccupationID);