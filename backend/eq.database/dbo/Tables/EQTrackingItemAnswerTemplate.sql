CREATE TABLE [dbo].[EQTrackingItemAnswerTemplate] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [TrackingItemId] INT            NOT NULL,
    [AnswerTypeId]   INT            NOT NULL,
    [Title]          NVARCHAR (MAX) NULL,
    [Point]          INT            NULL,
    CONSTRAINT [PK_EQTrackingItemAnswerTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EQTrackingItemAnswerTemplate_AnswerType] FOREIGN KEY ([AnswerTypeId]) REFERENCES [dbo].[AnswerType] ([Id]),
    CONSTRAINT [FK_EQTrackingItemAnswerTemplate_EQTrackingItemTemplate] FOREIGN KEY ([TrackingItemId]) REFERENCES [dbo].[EQTrackingItemTemplate] ([Id])
);

