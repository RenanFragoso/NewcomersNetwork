CREATE TABLE [dbo].[Settings_JustShareHome] (
    [JustShareGroup] NVARCHAR (128) NOT NULL,
    [ItemsNumber]    INT            NOT NULL,
    [Status]         NCHAR (1)      DEFAULT ('O') NOT NULL,
    PRIMARY KEY CLUSTERED ([JustShareGroup] ASC)
);

