CREATE TABLE [dbo].[Services] (
    [ServiceID]          NVARCHAR (128)  DEFAULT (newid()) NOT NULL,
    [ServiceName]        NVARCHAR (50)   NOT NULL,
    [ServiceDescription] NVARCHAR (1000) NOT NULL,
    [ServiceResponsible] NVARCHAR (128)  NULL,
    [ServiceCreateDate]  DATETIME        NULL,
    [ServiceAlterDate]   DATETIME        NULL,
    [ServiceGroup]       NVARCHAR (126)  NULL,
    [ServiceStatus]      CHAR (1)        NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Services provided by the NerwcomersNetwork', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Services';

