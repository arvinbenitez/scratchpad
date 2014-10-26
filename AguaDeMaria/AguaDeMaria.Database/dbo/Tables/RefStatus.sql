CREATE TABLE [dbo].[RefStatus] (
    [StatusId]   INT            IDENTITY (1, 1) NOT NULL,
    [StatusName] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_RefStatus] PRIMARY KEY CLUSTERED ([StatusId] ASC)
);

