using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class v133 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId2",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId2",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropColumn(
                name: "propiedadId2",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.AlterColumn<string>(
                name: "propiedadId1",
                table: "TipoPropiedadCaracteristica",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId1",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId1",
                principalTable: "Propiedad",
                principalColumn: "propiedadId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId1",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.AlterColumn<string>(
                name: "propiedadId1",
                table: "TipoPropiedadCaracteristica",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "propiedadId2",
                table: "TipoPropiedadCaracteristica",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId2",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId2");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId2",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId2",
                principalTable: "Propiedad",
                principalColumn: "propiedadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
