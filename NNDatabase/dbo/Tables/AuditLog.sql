CREATE TABLE [dbo].[AuditLog] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [DateCreated]  DATETIME       NOT NULL,
    [UserName]     NVARCHAR (256) NULL,
    [Message]      NVARCHAR (512) CONSTRAINT [ColumnDefault_bc64ff5e-30a0-4580-83dd-fe0114d2fff8] DEFAULT (NULL) NULL,
    [Category]     CHAR (3)       NOT NULL,
    [ExternalUser] NVARCHAR (256) NULL,
    [SessionID]    NVARCHAR (50)  NULL,
    [ClientIP]     NVARCHAR (50)  NULL,
    CONSTRAINT [PrimaryKey_0ec08ffc-9bb6-446c-9874-94d3612232a4] PRIMARY KEY CLUSTERED ([ID] ASC)
);

