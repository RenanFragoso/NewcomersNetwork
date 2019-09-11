CREATE TABLE [dbo].[Volunteer] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [DateCreated]         DATETIME       NOT NULL,
    [HomePhoneNumber]     BIGINT         NULL,
    [OtherPhoneNumber]    BIGINT         NULL,
    [Gender]              INT            NULL,
    [LanguagesSpoken]     NVARCHAR (100) NULL,
    [Status]              CHAR (1)       NOT NULL,
    [PostalCode]          CHAR (7)       NULL,
    [LastModified]        DATETIME       NOT NULL,
    [AgeRangeID]          INT            NULL,
    [NearestIntersection] NVARCHAR (256) NULL,
    [HasCar]              BIT            CONSTRAINT [ColumnDefault_6717549b-2601-48a6-9f9c-249ce23311ae] DEFAULT ((0)) NULL,
    [UserID]              INT            NOT NULL,
    [ReferenceID]         INT            NULL,
    CONSTRAINT [PrimaryKey_c717a1d6-5e51-4e74-9bfe-c2d16cb3c9cf] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Volunteer_0] FOREIGN KEY ([Gender]) REFERENCES [dbo].[Choices_Gender] ([ID])
);

