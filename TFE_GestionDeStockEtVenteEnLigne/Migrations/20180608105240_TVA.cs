using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TFE_GestionDeStockEtVenteEnLigne.Migrations
{
    public partial class TVA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TauxTVAID",
                table: "Provients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TauxTVAID",
                table: "Produits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TVA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valeur = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVA", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TVA");

            migrationBuilder.DropColumn(
                name: "TauxTVAID",
                table: "Provients");

            migrationBuilder.DropColumn(
                name: "TauxTVAID",
                table: "Produits");
        }
    }
}
