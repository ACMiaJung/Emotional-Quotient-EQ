CREATE TABLE [dbo].[PersonalTrackingLog] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [UserId]   NVARCHAR (128) NULL,
    [AnswerId] INT            NOT NULL,
    [Point]    INT            NOT NULL,
    [Comment]  NVARCHAR (MAX) NULL,
    [RegDT]    DATETIME       CONSTRAINT [DF_PersonalTrackingLog_RegDT] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PersonalTrackingLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonalTrackingLog_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_PersonalTrackingLog_EQTrackingItemAnswer] FOREIGN KEY ([AnswerId]) REFERENCES [dbo].[EQTrackingItemAnswer] ([Id])
);

