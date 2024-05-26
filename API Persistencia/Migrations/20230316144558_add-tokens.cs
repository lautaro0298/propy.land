using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class addtokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumns(
                new string[] { "CreatedAt", "ExternalId" },
                table: "Usuario");

            migrationBuilder.AddColumns(
                new[] { "AccessToken", "ExpirationTime", "RefreshToken" },
                table: "Usuario",
                new[] { columnType: "nvarchar(100)", nullable: true, columnType: "datetime2", nullable: true, columnType: "nvarchar(100)", nullable: true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumns(
                new string[] { "AccessToken", "ExpirationTime", "RefreshToken" },
                table: "Usuario");

            migrationBuilder.AddColumns(
                new[] { "CreatedAt", "ExternalId" },
                table: "Usuario",
                new[] { columnType: "datetime2", nullable: false, columnType: "nvarchar(max)", nullable: true });
        }
    }
}
