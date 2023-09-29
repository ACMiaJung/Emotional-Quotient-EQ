CREATE TABLE [dbo].[AnswerType] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [TypeName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AnswerType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

