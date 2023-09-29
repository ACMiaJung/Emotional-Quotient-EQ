CREATE TABLE [dbo].[EQTrackingItem] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [AreaId]             INT            NOT NULL,
    [UserId]             NVARCHAR (128) NOT NULL,
    [Title]              NVARCHAR (100) NOT NULL,
    [Question]           NVARCHAR (MAX) NOT NULL,
    [OriginalTemplateId] INT            NULL,
    CONSTRAINT [PK_EQTrackingItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EQTrackingItem_AspNetUsers1] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_EQTrackingItem_EQArea] FOREIGN KEY ([AreaId]) REFERENCES [dbo].[EQArea] ([Id])
);

