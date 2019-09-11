CREATE TABLE [dbo].[Mentee] (
    [ID]                               INT             IDENTITY (1, 1) NOT NULL,
    [UserID]                           INT             NOT NULL,
    [LevelOfEducation]                 NVARCHAR (200)  NULL,
    [HaveCredentialsAssessedInCAD]     BIT             NULL,
    [CredentialsAssessedInCAD_Where]   NVARCHAR (50)   NULL,
    [IsCurrentlyWorking]               BIT             NULL,
    [CurrentlyWorking_HoursPerWeek]    INT             NULL,
    [HavePreviouslyWorkedInFieldInCAD] BIT             NULL,
    [PastWorkExperience]               NVARCHAR (1000) NULL,
    [HaveCompletedJobPrepProgram]      BIT             NULL,
    [CompletedJobPrepProgram_Where]    NVARCHAR (50)   NULL,
    [Profession]                       NVARCHAR (100)  NULL,
    [Specialization]                   NVARCHAR (100)  NULL,
    [OccupationID]                     INT             NULL,
    CONSTRAINT [PrimaryKey_2043f304-5bc0-4e90-8088-692d7e0724ee] PRIMARY KEY CLUSTERED ([ID] ASC)
);

