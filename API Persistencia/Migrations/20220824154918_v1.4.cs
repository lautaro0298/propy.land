using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propiedad_TipoPropiedad_tipoPropiedadId",
                table: "Propiedad");

            migrationBuilder.DropIndex(
                name: "IX_Propiedad_tipoPropiedadId",
                table: "Propiedad");

            migrationBuilder.AlterColumn<string>(
                name: "tipoPropiedadId",
                table: "Propiedad",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tipoPropiedadId",
                table: "Propiedad",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_tipoPropiedadId",
                table: "Propiedad",
                column: "tipoPropiedadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedad_TipoPropiedad_tipoPropiedadId",
                table: "Propiedad",
                column: "tipoPropiedadId",
                principalTable: "TipoPropiedad",
                principalColumn: "tipoPropiedadId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
