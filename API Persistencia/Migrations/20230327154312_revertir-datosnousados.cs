using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class revertirdatosnousados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "ExpirationTime",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationTime",
                table: "Usuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
