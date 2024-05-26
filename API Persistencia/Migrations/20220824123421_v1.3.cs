using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class v13_Improved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key and index on TipoPropiedadCaracteristica.propiedadId1
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId1",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica");

            // Change the type of propiedadId1 to be nullable
            migrationBuilder.AlterColumn<string>(
                name: "propiedadId1",
                table: "TipoPropiedadCaracteristica",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Add a new column propiedadId2 to TipoPropiedadCaracteristica
            migrationBuilder.AddColumn<string>(
                name: "propiedadId2",
                table: "TipoPropiedadCaracteristica",
                nullable: false,
                defaultValue: "");

            // Change the type of tipoPropiedadId to be nullable
            migrationBuilder.AlterColumn<string>(
                name: "tipoPropiedadId",
                table: "Propiedad",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            // Create a new index on TipoPropiedadCaracteristica.propiedadId2
            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId2",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId2");

            // Create a new index on Propiedad.tipoPropiedadId
            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_tipoPropiedadId",
                table: "Propiedad",
                column: "tipoPropiedadId");

            // Add a new foreign key from Propiedad to TipoPropiedad
            migrationBuilder.AddForeignKey(
                name: "FK_Propiedad_TipoPropiedad_tipoPropiedadId",
                table: "Propiedad",
                column: "tipoPropiedadId",
                principalTable: "TipoPropiedad",
                principalColumn: "tipoPropiedadId",
                onDelete: ReferentialAction.Restrict);

            // Add a new foreign key from TipoPropiedadCaracteristica to Propiedad
            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId2",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId2",
                principalTable: "Propiedad",
                principalColumn: "propiedadId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key from TipoPropiedadCaracteristica to Propiedad
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId2",
                table: "TipoPropiedadCaracteristica");

            // Drop the foreign key from Propiedad to TipoPropiedad
            migrationBuilder.DropForeignKey(
                name: "FK_Propiedad_TipoPropiedad_tipoPropiedadId",
                table: "Propiedad");

            // Drop the index on Propiedad.tipoPropiedadId
            migrationBuilder.DropIndex(
                name: "IX_Propiedad_tipoPropiedadId",
                table: "Propiedad");

            // Drop the index on TipoPropiedadCaracteristica.propiedadId2
            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId2",
                table: "TipoPropiedadCaracteristica");

            // Remove the propiedadId2 column from TipoPropiedadCaracteristica
            migrationBuilder.DropColumn(
                name: "propiedadId2",
                table: "TipoPropiedadCaracteristica");

            // Change the type of propiedadId1 to be non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "propiedadId1",
                table: "TipoPropiedadCaracteristica",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            // Change the type of tipoPropiedadId to be nullable
            migrationBuilder.AlterColumn<string>(
                name: "tipoPropiedadId",
                table: "Propiedad",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            // Create a new index on TipoPropiedadCaracteristica.propiedadId1
            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId1");

            // Add a new foreign key from TipoPropiedadCaracteristica to Propiedad
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
