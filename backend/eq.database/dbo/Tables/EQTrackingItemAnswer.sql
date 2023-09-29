CREATE TABLE [dbo].[EQTrackingItemAnswer] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [TrackingItemId] INT            NOT NULL,
    [AnswerTypeId]   INT            NOT NULL,
    [Title]          NVARCHAR (MAX) NULL,
    [Point]          INT            NULL,
    CONSTRAINT [PK_EQTrackingItemAnswer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EQTrackingItemAnswer_AnswerType] FOREIGN KEY ([AnswerTypeId]) REFERENCES [dbo].[AnswerType] ([Id]),
    CONSTRAINT [FK_EQTrackingItemAnswer_EQTrackingItem] FOREIGN KEY ([TrackingItemId]) REFERENCES [dbo].[EQTrackingItem] ([Id])
);

