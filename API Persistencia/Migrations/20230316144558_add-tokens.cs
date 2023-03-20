using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class addtokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Usuario",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationTime",
                table: "Usuario",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Usuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Usuario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
