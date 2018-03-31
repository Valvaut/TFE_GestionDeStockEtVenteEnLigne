using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TFE_GestionDeStockEtVenteEnLigne.Migrations
{
    public partial class client03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Clients_ClientID",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_Domiciles_Clients_ClientID",
                table: "Domiciles");

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Domiciles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Commandes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Clients_ClientID",
                table: "Commandes",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Domiciles_Clients_ClientID",
                table: "Domiciles",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Clients_ClientID",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_Domiciles_Clients_ClientID",
                table: "Domiciles");

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Domiciles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Commandes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Clients_ClientID",
                table: "Commandes",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Domiciles_Clients_ClientID",
                table: "Domiciles",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
