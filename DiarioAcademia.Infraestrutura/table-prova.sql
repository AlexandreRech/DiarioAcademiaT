CREATE TABLE [dbo].[TBProva] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [Data]              DATETIME2 (7) NOT NULL,
    [Assunto]           VARCHAR (MAX) NOT NULL,
    [FeedbackRealizado] BIT           NOT NULL,
    [Gabarito]          VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);