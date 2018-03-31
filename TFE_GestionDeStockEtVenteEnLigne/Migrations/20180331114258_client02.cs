using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TFE_GestionDeStockEtVenteEnLigne.Migrations
{
    public partial class client02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegisterViewModelID",
                table: "Domiciles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterViewModelID",
                table: "Commandes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterViewModelID",
                table: "Domiciles");

            migrationBuilder.DropColumn(
                name: "RegisterViewModelID",
                table: "Commandes");
        }
    }
}
