using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class v166 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicacionCaracteristica_Caracteristica_CaracteristicaId",
                table: "PublicacionCaracteristica");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicacionCaracteristica_Publicacion_PublicacionId",
                table: "PublicacionCaracteristica");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicacionCaracteristica",
                table: "PublicacionCaracteristica");

            migrationBuilder.RenameTable(
                name: "PublicacionCaracteristica",
                newName: "PublicacionCaracteristicas");

            migrationBuilder.RenameIndex(
                name: "IX_PublicacionCaracteristica_CaracteristicaId",
                table: "PublicacionCaracteristicas",
                newName: "IX_PublicacionCaracteristicas_CaracteristicaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicacionCaracteristicas",
                table: "PublicacionCaracteristicas",
                columns: new[] { "PublicacionId", "CaracteristicaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PublicacionCaracteristicas_Caracteristica_CaracteristicaId",
                table: "PublicacionCaracteristicas",
                column: "CaracteristicaId",
                principalTable: "Caracteristica",
                principalColumn: "caracteristicaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicacionCaracteristicas_Publicacion_PublicacionId",
                table: "PublicacionCaracteristicas",
                column: "PublicacionId",
                principalTable: "Publicacion",
                principalColumn: "publicacionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicacionCaracteristicas_Caracteristica_CaracteristicaId",
                table: "PublicacionCaracteristicas");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicacionCaracteristicas_Publicacion_PublicacionId",
                table: "PublicacionCaracteristicas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicacionCaracteristicas",
                table: "PublicacionCaracteristicas");

            migrationBuilder.RenameTable(
                name: "PublicacionCaracteristicas",
                newName: "PublicacionCaracteristica");

            migrationBuilder.RenameIndex(
                name: "IX_PublicacionCaracteristicas_CaracteristicaId",
                table: "PublicacionCaracteristica",
                newName: "IX_PublicacionCaracteristica_CaracteristicaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicacionCaracteristica",
                table: "PublicacionCaracteristica",
                columns: new[] { "PublicacionId", "CaracteristicaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PublicacionCaracteristica_Caracteristica_CaracteristicaId",
                table: "PublicacionCaracteristica",
                column: "CaracteristicaId",
                principalTable: "Caracteristica",
                principalColumn: "caracteristicaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicacionCaracteristica_Publicacion_PublicacionId",
                table: "PublicacionCaracteristica",
                column: "PublicacionId",
                principalTable: "Publicacion",
                principalColumn: "publicacionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
