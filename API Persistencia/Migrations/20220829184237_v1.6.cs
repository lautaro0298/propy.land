using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PublicacionCaracteristica",
                columns: table => new
                {
                    CaracteristicaId = table.Column<string>(nullable: false),
                    PublicacionId = table.Column<string>(nullable: false),
                    PublicacionCaracteristicaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicacionCaracteristica", x => new { x.PublicacionId, x.CaracteristicaId });
                    table.ForeignKey(
                        name: "FK_PublicacionCaracteristica_Caracteristica_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristica",
                        principalColumn: "caracteristicaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicacionCaracteristica_Publicacion_PublicacionId",
                        column: x => x.PublicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "publicacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionCaracteristica_CaracteristicaId",
                table: "PublicacionCaracteristica",
                column: "CaracteristicaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicacionCaracteristica");

            migrationBuilder.AddColumn<string>(
                name: "PublicacionspublicacionId",
                table: "TipoPropiedadCaracteristica",
                type: "nvarchar(450)",
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
    }
}
