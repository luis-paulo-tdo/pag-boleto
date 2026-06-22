using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PagBoleto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "boletos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    linha_digitavel = table.Column<string>(type: "character varying(54)", maxLength: 54, nullable: false),
                    valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    vencimento = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    tentativas_processamento = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    motivo_falha = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boletos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    conteudo = table.Column<string>(type: "jsonb", nullable: false),
                    criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    processado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_boletos_linha_digitavel",
                table: "boletos",
                column: "linha_digitavel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_boletos_status_vencimento",
                table: "boletos",
                columns: new[] { "status", "vencimento" });

            migrationBuilder.CreateIndex(
                name: "ix_outbox_message_processado_criado_em",
                table: "outbox_messages",
                columns: new[] { "processado_em", "criado_em" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "boletos");

            migrationBuilder.DropTable(
                name: "outbox_messages");
        }
    }
}
