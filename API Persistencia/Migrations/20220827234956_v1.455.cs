using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class v1455 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Caracteristica_propiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId1",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPropiedadCaracteristica",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropColumn(
                name: "propiedadId1",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.AlterColumn<string>(
                name: "propiedadId",
                table: "TipoPropiedadCaracteristica",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "TipopropiedadId",
                table: "TipoPropiedadCaracteristica",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPropiedadCaracteristica",
                table: "TipoPropiedadCaracteristica",
                columns: new[] { "TipopropiedadId", "caracteristicaId" });

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Caracteristica_TipopropiedadId",
                table: "TipoPropiedadCaracteristica",
                column: "TipopropiedadId",
                principalTable: "Caracteristica",
                principalColumn: "caracteristicaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId",
                principalTable: "Propiedad",
                principalColumn: "propiedadId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Caracteristica_TipopropiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPropiedadCaracteristica",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropColumn(
                name: "TipopropiedadId",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.AlterColumn<string>(
                name: "propiedadId",
                table: "TipoPropiedadCaracteristica",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "propiedadId1",
                table: "TipoPropiedadCaracteristica",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPropiedadCaracteristica",
                table: "TipoPropiedadCaracteristica",
                columns: new[] { "propiedadId", "caracteristicaId" });

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Caracteristica_propiedadId",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId",
                principalTable: "Caracteristica",
                principalColumn: "caracteristicaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId1",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId1",
                principalTable: "Propiedad",
                principalColumn: "propiedadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
