--insert into TA_Tipo_Cod_Retorno values('Sinistro informado não foi encontrado.'); --1

CREATE TABLE TB_Log_Fonetica (
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DT_Cadastro] [datetime] NOT NULL,
    [NM_Palavra] [varchar](20) NULL,
    [NM_Fonetica] [varchar](20) NULL,
    [DT_Retorno] [datetime]NULL,
    [CD_Retorno] [int] NULL
     
 CONSTRAINT [PK_TB_Log_Desvincular_Sinistro] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
)
GO
ALTER TABLE [dbo].[TB_Log_Fonetica]  WITH NOCHECK ADD  CONSTRAINT [FK_TB_Log_Fonetica_TA_Tipo_Cod_Retorno] FOREIGN KEY([CD_Retorno])
REFERENCES [dbo].[TA_Tipo_Cod_Retorno] ([ID])
GO
