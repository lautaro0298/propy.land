using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using API_Persistencia.Models;

namespace API_Persistencia.Migrations
{
    /// <summary>
    /// Migration for ignoring add operations
    /// </summary>
    public partial class IgnoreAdd : Migration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreAdd"/> class.
        /// </summary>
        public IgnoreAdd()
        {
            // Set the product version
            this.ProductVersion = "3.1.5";
        }

        /// <summary>
        /// Gets or sets the product version.
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// Applies the migration.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.HasAnnotation("ProductVersion", this.ProductVersion);
            migrationBuilder.HasAnnotation("Relational:MaxIdentifierLength", 128);
            migrationBuilder.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.Entity<Actividad>(entity =>
            {
                entity.Property(e => e.ActividadId)
                    .HasColumnType("nvarchar(450)");

                entity.Property(e => e.DescripcionActividad)
                    .HasColumnType("nvarchar(max)");

                entity.Property(e => e.FechaActividad)
                    .HasColumnType("datetime2");

                entity.Property(e => e.UsuarioId)
                    .HasColumnType("nvarchar(450)");

                entity.HasKey(e => e.ActividadId);

                entity.HasIndex(e => e.UsuarioId);

                entity.ToTable("Actividad");
            });

            // ... (other entities)

            // Add foreign keys and relationships
            migrationBuilder.CreateIndex(
                name: "IX_Favorito_PublicacionId",
                table: "Favorito",
                column: "PublicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_UsuarioId",
                table: "Favorito",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorito_Publicacion_PublicacionId",
                table: "Favorito",
                column: "PublicacionId",
                principalTable: "Publicacion",
                principalColumn: "PublicacionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorito_Usuario_UsuarioId",
                table: "Favorito",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            // ... (other foreign keys and relationships)
        }

        /// <summary>
        /// Unapplies the migration.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorito_Publicacion_PublicacionId",
                table: "Favorito");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorito_Usuario_UsuarioId",
                table: "Favorito");

            migrationBuilder.DropIndex(
                name: "IX_Favorito_PublicacionId",
                table: "Favorito");

            migrationBuilder.DropIndex(
                name: "IX_Favorito_UsuarioId",
                table: "Favorito");

            // ... (other foreign keys and relationships)

            migrationBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.ActividadId);

                entity.HasIndex(e => e.UsuarioId);

                entity.ToTable("Actividad");
            });

            // ... (other entities)
        }
    }
}
