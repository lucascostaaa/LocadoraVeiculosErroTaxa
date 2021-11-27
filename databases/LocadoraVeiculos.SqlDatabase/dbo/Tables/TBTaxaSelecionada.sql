CREATE TABLE [dbo].[TBTaxaSelecionada] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [Locacao_Id] INT NOT NULL,
    [Taxa_Id]    INT NOT NULL,
    CONSTRAINT [PK_TBTaxaSelecionada] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBLocacao_TBTaxasServicos_TBLocacao] FOREIGN KEY ([Locacao_Id]) REFERENCES [dbo].[TBLocacao] ([Id]),
    CONSTRAINT [FK_TBLocacao_TBTaxasServicos_TBTaxasServicos] FOREIGN KEY ([Taxa_Id]) REFERENCES [dbo].[TBTaxa] ([Id])
);

