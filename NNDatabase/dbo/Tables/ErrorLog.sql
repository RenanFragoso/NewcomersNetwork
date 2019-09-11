CREATE TABLE [dbo].[ErrorLog] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [DateCreated] DATETIME       NOT NULL,
    [Message]     NVARCHAR (MAX) NOT NULL,
    [Context]     NVARCHAR (512) NULL,
    CONSTRAINT [PrimaryKey_c7d05b5e-dcdf-414e-8672-236dfcb9eb50] PRIMARY KEY CLUSTERED ([ID] ASC)
);

