using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class newrelation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Caracteristica_TipopropiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_TipoPropiedad_caracteristicaId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPropiedadCaracteristica",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_caracteristicaId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPropiedadCaracteristica",
                table: "TipoPropiedadCaracteristica",
                columns: new[] { "caracteristicaId", "TipopropiedadId" });

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_TipopropiedadId",
                table: "TipoPropiedadCaracteristica",
                column: "TipopropiedadId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_TipoPropiedad_TipopropiedadId",
                table: "TipoPropiedadCaracteristica",
                column: "TipopropiedadId",
                principalTable: "TipoPropiedad",
                principalColumn: "tipoPropiedadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Caracteristica_caracteristicaId",
                table: "TipoPropiedadCaracteristica",
                column: "caracteristicaId",
                principalTable: "Caracteristica",
                principalColumn: "caracteristicaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_TipoPropiedad_TipopropiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Caracteristica_caracteristicaId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPropiedadCaracteristica",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_TipopropiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPropiedadCaracteristica",
                table: "TipoPropiedadCaracteristica",
                columns: new[] { "TipopropiedadId", "caracteristicaId" });

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_caracteristicaId",
                table: "TipoPropiedadCaracteristica",
                column: "caracteristicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Caracteristica_TipopropiedadId",
                table: "TipoPropiedadCaracteristica",
                column: "TipopropiedadId",
                principalTable: "Caracteristica",
                principalColumn: "caracteristicaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_TipoPropiedad_caracteristicaId",
                table: "TipoPropiedadCaracteristica",
                column: "caracteristicaId",
                principalTable: "TipoPropiedad",
                principalColumn: "tipoPropiedadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
