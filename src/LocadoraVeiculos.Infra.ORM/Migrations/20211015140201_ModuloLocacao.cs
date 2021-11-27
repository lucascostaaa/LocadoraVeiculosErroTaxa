using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LocadoraVeiculos.Infra.ORM.Migrations
{
    public partial class ModuloLocacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    RG = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CPF = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TipoPessoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBFuncionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Usuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Senha = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DataAdmissao = table.Column<DateTime>(type: "date", nullable: false),
                    Salario = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBFuncionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBGrupoVeiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBGrupoVeiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBParceiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBParceiro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBTaxa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TipoTaxa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoTaxaLocacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTaxa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBCondutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Endereco = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    RG = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    CPF = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CNH = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    DataValidadeCNH = table.Column<DateTime>(type: "date", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCondutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBCondutor_TBCliente",
                        column: x => x.ClienteId,
                        principalTable: "TBCliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBPlanoCobranca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorDia = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    KilometragemLivreInclusa = table.Column<int>(type: "int", nullable: false),
                    ValorKMRodado = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TipoPlano = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrupoVeiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPlanoCobranca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBPlanoCobranca_TBGrupoVeiculo",
                        column: x => x.GrupoVeiculoId,
                        principalTable: "TBGrupoVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBVeiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TipoCombustivel = table.Column<int>(type: "int", unicode: false, maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Fabricante = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Quilometragem = table.Column<double>(type: "float", nullable: false),
                    QtdLitrosTanque = table.Column<int>(type: "int", nullable: false),
                    QtdPortas = table.Column<int>(type: "int", nullable: false),
                    NumeroChassi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Cor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CapacidadeOcupantes = table.Column<int>(type: "int", nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    TamanhoPortaMalas = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Imagem = table.Column<byte[]>(type: "image", nullable: true),
                    GrupoVeiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBVeiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBVeiculo_TBGrupoVeiculo",
                        column: x => x.GrupoVeiculoId,
                        principalTable: "TBGrupoVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBCupom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValue: 0m),
                    DataValidade = table.Column<DateTime>(type: "date", nullable: false),
                    ParceiroId = table.Column<int>(type: "int", nullable: true),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValue: 0m),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCupom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBCupom_TBParceiro_ParceiroId",
                        column: x => x.ParceiroId,
                        principalTable: "TBParceiro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBLocacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataLocacao = table.Column<DateTime>(type: "date", nullable: false),
                    DataDevolucaoPrevista = table.Column<DateTime>(type: "date", nullable: false),
                    DataDevolucaoRealizada = table.Column<DateTime>(type: "date", nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false),
                    VeiculoId = table.Column<int>(type: "int", nullable: false),
                    CondutorId = table.Column<int>(type: "int", nullable: false),
                    PlanoCobrancaId = table.Column<int>(type: "int", nullable: false),
                    MarcadorCombustivel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CupomId = table.Column<int>(type: "int", nullable: true),
                    SituacaoEnvioEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmAberto = table.Column<bool>(type: "bit", nullable: false),
                    QuilometragemPercorrida = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Relatorio = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBLocacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBLocacao_TBCondutor",
                        column: x => x.CondutorId,
                        principalTable: "TBCondutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBLocacao_TBCupom",
                        column: x => x.CupomId,
                        principalTable: "TBCupom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBLocacao_TBFuncionario",
                        column: x => x.FuncionarioId,
                        principalTable: "TBFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBLocacao_TBPlanoCobranca",
                        column: x => x.PlanoCobrancaId,
                        principalTable: "TBPlanoCobranca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBLocacao_TBVeiculos",
                        column: x => x.VeiculoId,
                        principalTable: "TBVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocacaoTaxa",
                columns: table => new
                {
                    LocacoesId = table.Column<int>(type: "int", nullable: false),
                    TaxasSelecionadasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocacaoTaxa", x => new { x.LocacoesId, x.TaxasSelecionadasId });
                    table.ForeignKey(
                        name: "FK_LocacaoTaxa_TBLocacao_LocacoesId",
                        column: x => x.LocacoesId,
                        principalTable: "TBLocacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocacaoTaxa_TBTaxa_TaxasSelecionadasId",
                        column: x => x.TaxasSelecionadasId,
                        principalTable: "TBTaxa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoTaxa_TaxasSelecionadasId",
                table: "LocacaoTaxa",
                column: "TaxasSelecionadasId");

            migrationBuilder.CreateIndex(
                name: "IX_TBCondutor_ClienteId",
                table: "TBCondutor",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TBCupom_ParceiroId",
                table: "TBCupom",
                column: "ParceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_TBLocacao_CondutorId",
                table: "TBLocacao",
                column: "CondutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TBLocacao_CupomId",
                table: "TBLocacao",
                column: "CupomId");

            migrationBuilder.CreateIndex(
                name: "IX_TBLocacao_FuncionarioId",
                table: "TBLocacao",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TBLocacao_PlanoCobrancaId",
                table: "TBLocacao",
                column: "PlanoCobrancaId");

            migrationBuilder.CreateIndex(
                name: "IX_TBLocacao_VeiculoId",
                table: "TBLocacao",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_TBPlanoCobranca_GrupoVeiculoId",
                table: "TBPlanoCobranca",
                column: "GrupoVeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_TBVeiculo_GrupoVeiculoId",
                table: "TBVeiculo",
                column: "GrupoVeiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocacaoTaxa");

            migrationBuilder.DropTable(
                name: "TBLocacao");

            migrationBuilder.DropTable(
                name: "TBTaxa");

            migrationBuilder.DropTable(
                name: "TBCondutor");

            migrationBuilder.DropTable(
                name: "TBCupom");

            migrationBuilder.DropTable(
                name: "TBFuncionario");

            migrationBuilder.DropTable(
                name: "TBPlanoCobranca");

            migrationBuilder.DropTable(
                name: "TBVeiculo");

            migrationBuilder.DropTable(
                name: "TBCliente");

            migrationBuilder.DropTable(
                name: "TBParceiro");

            migrationBuilder.DropTable(
                name: "TBGrupoVeiculo");
        }
    }
}
