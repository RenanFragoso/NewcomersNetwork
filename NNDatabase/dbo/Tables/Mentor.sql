CREATE TABLE [dbo].[Mentor] (
    [ID]                              INT             IDENTITY (1, 1) NOT NULL,
    [Profession]                      NVARCHAR (100)  CONSTRAINT [ColumnDefault_e776c26e-ffc1-4ea2-9b6a-fc0acb175d4f] DEFAULT (NULL) NULL,
    [Specialization]                  NVARCHAR (100)  CONSTRAINT [ColumnDefault_8e989e6c-b190-4ef1-a7be-5b37753ed783] DEFAULT (NULL) NULL,
    [IsCurrentlyWorking]              BIT             CONSTRAINT [ColumnDefault_fc1f51e8-5424-4bc3-b65e-a1a373817b6f] DEFAULT (NULL) NULL,
    [CurrentJobTitle]                 NVARCHAR (50)   CONSTRAINT [ColumnDefault_5d939965-927c-456f-a4a9-f062692c18a7] DEFAULT (NULL) NULL,
    [Division]                        NVARCHAR (50)   CONSTRAINT [ColumnDefault_09135def-eb8a-47bc-a466-139653243a4b] DEFAULT (NULL) NULL,
    [WorkAddress]                     NVARCHAR (200)  CONSTRAINT [ColumnDefault_a51c60b3-5a33-421b-bf50-b8ff98a240b5] DEFAULT (NULL) NULL,
    [WorkPostalCode]                  NVARCHAR (10)   CONSTRAINT [ColumnDefault_7475c062-95a8-4008-a084-d3f54a08465c] DEFAULT (NULL) NULL,
    [BusinessTelephone]               INT             CONSTRAINT [ColumnDefault_dc1b3f43-3158-406d-a263-650bca767a05] DEFAULT (NULL) NULL,
    [ConsentToReceiveJVSEmail]        BIT             CONSTRAINT [ColumnDefault_5f515d82-9f4d-4c61-bfd0-38a0349451fd] DEFAULT (NULL) NULL,
    [Credentials_Degree]              NVARCHAR (100)  CONSTRAINT [ColumnDefault_a4b7ffdd-8eec-4637-acf6-684ccf7e5671] DEFAULT (NULL) NULL,
    [ProfessionalAffiliations]        NVARCHAR (50)   CONSTRAINT [ColumnDefault_bf145b61-ffbb-4b15-a379-0576f8b74ad5] DEFAULT (NULL) NULL,
    [Other]                           NVARCHAR (100)  CONSTRAINT [ColumnDefault_55ae63e4-514d-42d5-8d66-7a71b436d880] DEFAULT (NULL) NULL,
    [HowLongInProfessionInCAD]        INT             CONSTRAINT [ColumnDefault_23413625-e93c-4662-a8d1-fce4539cf7a0] DEFAULT (NULL) NULL,
    [WorkExperience]                  NVARCHAR (2000) CONSTRAINT [ColumnDefault_d47522d3-566f-48cc-84a6-f1d02267c3b7] DEFAULT (NULL) NULL,
    [SpecialSkillsHobbiesOrInterests] NVARCHAR (500)  CONSTRAINT [ColumnDefault_c05b3221-6e1b-45f4-b43d-7b915cc1cf4a] DEFAULT (NULL) NULL,
    [WhyVolunteer]                    NVARCHAR (500)  CONSTRAINT [ColumnDefault_65888ad9-bba6-4385-8d62-14f497af6674] DEFAULT (NULL) NULL,
    [UserID]                          INT             NULL,
    CONSTRAINT [PrimaryKey_c88792e4-dd07-44b9-8aa5-b3d7cf65855e] PRIMARY KEY CLUSTERED ([ID] ASC)
);

