CREATE TABLE [dbo].[UserDetails] (
    [Id]                  NVARCHAR (128) NOT NULL,
    [FirstName]           NVARCHAR (100) NOT NULL,
    [Email]               NVARCHAR (100) NOT NULL,
    [Status]              CHAR (1)       NOT NULL,
    [DateCreated]         DATETIME       NOT NULL,
    [LastModified]        DATETIME       NOT NULL,
    [LastName]            NVARCHAR (100) NOT NULL,
    [Gender]              NVARCHAR (1)   NULL,
    [MaritalStatus]       NVARCHAR (1)   NULL,
    [AgeRange]            NVARCHAR (2)   NULL,
    [Education]           NVARCHAR (2)   NULL,
    [NearestIntersection] NVARCHAR (256) NULL,
    [PostalCode]          NVARCHAR (7)   NULL,
    [ConsentToContact]    BIT            CONSTRAINT [DF_UserDetails_ConsentToContact] DEFAULT ((0)) NULL,
    [IsImmigrant]         BIT            CONSTRAINT [DF_UserDetails_IsImmigrant] DEFAULT ((1)) NULL,
    [Title]               NVARCHAR (50)  NULL,
    CONSTRAINT [PK_dbo.UserDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

