using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class v151 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicacion_TipoPropiedadCaracteristica_TipoPropiedadCaracteristicaTipopropiedadId_TipoPropiedadCaracteristicacaracteristica~",
                table: "Publicacion");

            migrationBuilder.DropIndex(
                name: "IX_Publicacion_TipoPropiedadCaracteristicaTipopropiedadId_TipoPropiedadCaracteristicacaracteristicaId",
                table: "Publicacion");

            migrationBuilder.DropColumn(
                name: "TipoPropiedadCaracteristicaTipopropiedadId",
                table: "Publicacion");

            migrationBuilder.DropColumn(
                name: "TipoPropiedadCaracteristicacaracteristicaId",
                table: "Publicacion");

            migrationBuilder.AddColumn<string>(
                name: "PublicacionspublicacionId",
                table: "TipoPropiedadCaracteristica",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_PublicacionspublicacionId",
                table: "TipoPropiedadCaracteristica",
                column: "PublicacionspublicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Publicacion_PublicacionspublicacionId",
                table: "TipoPropiedadCaracteristica",
                column: "PublicacionspublicacionId",
                principalTable: "Publicacion",
                principalColumn: "publicacionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Publicacion_PublicacionspublicacionId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_PublicacionspublicacionId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropColumn(
                name: "PublicacionspublicacionId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.AddColumn<string>(
                name: "TipoPropiedadCaracteristicaTipopropiedadId",
                table: "Publicacion",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoPropiedadCaracteristicacaracteristicaId",
                table: "Publicacion",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publicacion_TipoPropiedadCaracteristicaTipopropiedadId_TipoPropiedadCaracteristicacaracteristicaId",
                table: "Publicacion",
                columns: new[] { "TipoPropiedadCaracteristicaTipopropiedadId", "TipoPropiedadCaracteristicacaracteristicaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Publicacion_TipoPropiedadCaracteristica_TipoPropiedadCaracteristicaTipopropiedadId_TipoPropiedadCaracteristicacaracteristica~",
                table: "Publicacion",
                columns: new[] { "TipoPropiedadCaracteristicaTipopropiedadId", "TipoPropiedadCaracteristicacaracteristicaId" },
                principalTable: "TipoPropiedadCaracteristica",
                principalColumns: new[] { "TipopropiedadId", "caracteristicaId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
