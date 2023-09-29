CREATE TABLE [dbo].[PersonalGoal] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [UserId]   NVARCHAR (128) NOT NULL,
    [AnswerId] INT            NOT NULL,
    [Goal]     INT            NOT NULL,
    CONSTRAINT [PK_PersonalGoal] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonalGoal_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_PersonalGoal_EQTrackingItemAnswer] FOREIGN KEY ([AnswerId]) REFERENCES [dbo].[EQTrackingItemAnswer] ([Id])
);

