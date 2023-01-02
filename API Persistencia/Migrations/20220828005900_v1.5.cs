using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropColumn(
                name: "propiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.AddColumn<string>(
                name: "TipoPropiedadCaracteristicaTipopropiedadId",
                table: "Publicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoPropiedadCaracteristicacaracteristicaId",
                table: "Publicacion",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "propiedadId",
                table: "TipoPropiedadCaracteristica",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId",
                principalTable: "Propiedad",
                principalColumn: "propiedadId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
