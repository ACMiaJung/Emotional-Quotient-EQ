CREATE TABLE [dbo].[EQTrackingItemTemplate] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [AreaId]   INT            NOT NULL,
    [Title]    NVARCHAR (100) NOT NULL,
    [Question] NVARCHAR (MAX) NOT NULL,
    [OwnerId]  NVARCHAR (128) NULL,
    CONSTRAINT [PK_EQTrackingItemTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EQTrackingItemTemplate_AspNetUsers] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_EQTrackingItemTemplate_EQArea] FOREIGN KEY ([AreaId]) REFERENCES [dbo].[EQArea] ([Id])
);

