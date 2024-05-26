using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class v133 : Migration
    {
        /// <summary>
        /// This method is called when the migration is applied.
        /// </summary>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove the foreign key constraint and index on propiedadId2
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_propiedadId2",
                table: "TipoPropiedadCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId2",
                table: "TipoPropiedadCaracteristica");

            // Remove the propiedadId2 column
            migrationBuilder.DropColumn(
                name: "propiedadId2",
                table: "TipoPropiedadCaracteristica");

            // Change the nullability of propiedadId1 to nullable
            migrationBuilder.AlterColumn<string>(
                name: "propiedadId1",
                table: "TipoPropiedadCaracteristica",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            // Create a new index on propiedadId1
            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId1");

            // Add a foreign key constraint on propiedadId1
            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId1",
                principalTable: "Propiedad",
                principalColumn: "propiedadId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <summary>
        /// This method is called when the migration is rolled back.
        /// </summary>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the foreign key constraint on propiedadId1
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica");

            // Remove the index on propiedadId1
            migrationBuilder.DropIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica");

            // Change the nullability of propiedadId1 back to non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "propiedadId1",
                table: "TipoPropiedadCaracteristica",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            // Add the propiedadId2 column
            migrationBuilder.AddColumn<string>(
                name: "propiedadId2",
                table: "TipoPropiedadCaracteristica",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            // Create a new index on propiedadId2
            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId2",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId2");

            // Add a foreign key constraint on propiedadId2
            migrationBuilder.AddForeignKey(
                name: "FK_TipoPropiedadCaracteristica_propiedadId2",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId2",
                principalTable: "Propiedad",
                principalColumn: "propiedadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
