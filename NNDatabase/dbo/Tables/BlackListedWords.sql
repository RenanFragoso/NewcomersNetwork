CREATE TABLE [dbo].[BlackListedWords] (
    [Word]   NVARCHAR (50) NOT NULL,
    [Status] NCHAR (1)     DEFAULT ('O') NOT NULL,
    PRIMARY KEY CLUSTERED ([Word] ASC)
);

