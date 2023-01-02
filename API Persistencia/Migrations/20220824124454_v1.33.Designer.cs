﻿// <auto-generated />
using System;
using API_Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_Persistencia.Migrations
{
    [DbContext(typeof(ConexionDB))]
    [Migration("20220824124454_v1.33")]
    partial class v133
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API_Persistencia.Models.Actividad", b =>
                {
                    b.Property<string>("actividadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("descripcionActividad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaActividad")
                        .HasColumnType("datetime2");

                    b.Property<string>("usuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("actividadId");

                    b.HasIndex("usuarioId");

                    b.ToTable("Actividad");
                });

            modelBuilder.Entity("API_Persistencia.Models.Caracteristica", b =>
                {
                    b.Property<string>("caracteristicaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("nombreCaracteristica")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("caracteristicaId");

                    b.ToTable("Caracteristica");
                });

            modelBuilder.Entity("API_Persistencia.Models.Credito", b =>
                {
                    b.Property<string>("PaqueteID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int>("CantidadCreditos")
                        .HasColumnType("int");

                    b.Property<string>("NombrePack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TipoMonedaID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PaqueteID");

                    b.HasIndex("TipoMonedaID");

                    b.ToTable("Credito");
                });

            modelBuilder.Entity("API_Persistencia.Models.Favorito", b =>
                {
                    b.Property<string>("favoritoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("publicacionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("usuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("favoritoId");

                    b.HasIndex("publicacionId");

                    b.HasIndex("usuarioId");

                    b.ToTable("Favorito");
                });

            modelBuilder.Entity("API_Persistencia.Models.ImagenPropiedad", b =>
                {
                    b.Property<string>("ImagenPropiedadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("propiedadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("rutaImagenPropiedad")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImagenPropiedadId");

                    b.HasIndex("propiedadId");

                    b.ToTable("ImagenPropiedad");
                });

            modelBuilder.Entity("API_Persistencia.Models.PagoMP", b =>
                {
                    b.Property<string>("PagoMPId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("currency_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_approved")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_created")
                        .HasColumnType("datetime2");

                    b.Property<string>("idPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("operation_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payment_type_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("transaction_amount")
                        .HasColumnType("real");

                    b.HasKey("PagoMPId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("PagoMP");
                });

            modelBuilder.Entity("API_Persistencia.Models.Plan", b =>
                {
                    b.Property<string>("planId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Vencimiento")
                        .HasColumnType("int");

                    b.Property<bool>("accesoEstadisticasAvanzadas")
                        .HasColumnType("bit");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<int>("cantidadCreditosIniciales")
                        .HasColumnType("int");

                    b.Property<int>("cantidadMaxImagenesPermitidasPorPublicacion")
                        .HasColumnType("int");

                    b.Property<string>("nombrePlan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("permiteVideo")
                        .HasColumnType("bit");

                    b.Property<decimal>("precioPlan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("tipoMonedaId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("planId");

                    b.HasIndex("tipoMonedaId");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("API_Persistencia.Models.PlanUsuario", b =>
                {
                    b.Property<string>("planUsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreditoPaqueteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("datetime2");

                    b.Property<long>("NumFactura")
                        .HasColumnType("bigint");

                    b.Property<string>("PagoMPId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<int>("cantidadCreditosActivos")
                        .HasColumnType("int");

                    b.Property<DateTime>("fechaContratacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("planId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("usuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("planUsuarioId");

                    b.HasIndex("CreditoPaqueteId");

                    b.HasIndex("PagoMPId");

                    b.HasIndex("planId");

                    b.HasIndex("usuarioId");

                    b.ToTable("PlanUsuario");
                });

            modelBuilder.Entity("API_Persistencia.Models.Propiedad", b =>
                {
                    b.Property<string>("propiedadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AreaAdmNivel1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AreaAdmNivel2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("amueblado")
                        .HasColumnType("bit");

                    b.Property<int>("añosAntiguedad")
                        .HasColumnType("int");

                    b.Property<string>("descripcionPropiedad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("importeExpensasUltimoMes")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("latitud")
                        .HasColumnType("float");

                    b.Property<double>("longitud")
                        .HasColumnType("float");

                    b.Property<int>("nroPisos")
                        .HasColumnType("int");

                    b.Property<string>("pais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("precioPropiedad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("superficieCubierta")
                        .HasColumnType("int");

                    b.Property<int>("superficieTerreno")
                        .HasColumnType("int");

                    b.Property<string>("tipoConstruccionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tipoMonedaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tipoPropiedadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tipoPublicanteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ubicacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("propiedadId");

                    b.HasIndex("tipoConstruccionId");

                    b.HasIndex("tipoMonedaId");

                    b.HasIndex("tipoPropiedadId");

                    b.HasIndex("tipoPublicanteId");

                    b.HasIndex("usuarioId");

                    b.ToTable("Propiedad");
                });

            modelBuilder.Entity("API_Persistencia.Models.PropiedadTipoAmbiente", b =>
                {
                    b.Property<string>("propiedadTipoAmbienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<int>("cantidad")
                        .HasColumnType("int");

                    b.Property<string>("propiedadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tipoAmbienteId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("propiedadTipoAmbienteId");

                    b.HasIndex("propiedadId");

                    b.HasIndex("tipoAmbienteId");

                    b.ToTable("PropiedadTipoAmbiente");
                });

            modelBuilder.Entity("API_Persistencia.Models.Publicacion", b =>
                {
                    b.Property<string>("publicacionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("fechaFinPublicacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaInicioPublicacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("propiedadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tipoPublicacionId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("publicacionId");

                    b.HasIndex("propiedadId");

                    b.HasIndex("tipoPublicacionId");

                    b.ToTable("Publicacion");
                });

            modelBuilder.Entity("API_Persistencia.Models.SolicitudContactoVisitante", b =>
                {
                    b.Property<string>("solicitudContactoVisitanteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("cantidadVecesRealizoSolicitud")
                        .HasColumnType("int");

                    b.Property<DateTime>("fechaSolicitud")
                        .HasColumnType("datetime2");

                    b.Property<string>("publicacionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("usuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("solicitudContactoVisitanteId");

                    b.HasIndex("publicacionId");

                    b.HasIndex("usuarioId");

                    b.ToTable("SolicitudContactoVisitante");
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoAmbiente", b =>
                {
                    b.Property<string>("tipoAmbienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("nombreTipoAmbiente")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipoAmbienteId");

                    b.ToTable("TipoAmbiente");
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoConstruccion", b =>
                {
                    b.Property<string>("tipoConstruccionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("nombreTipoConstruccion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipoConstruccionId");

                    b.ToTable("TipoConstruccion");
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoMoneda", b =>
                {
                    b.Property<string>("tipoMonedaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("denominacionMoneda")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipoMonedaId");

                    b.ToTable("TipoMoneda");
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoPropiedad", b =>
                {
                    b.Property<string>("tipoPropiedadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("nombreTipoPropiedad")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipoPropiedadId");

                    b.ToTable("TipoPropiedad");
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoPropiedadCaracteristica", b =>
                {
                    b.Property<string>("propiedadId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("caracteristicaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("propiedadId1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tipoPropiedadCaracteristicaID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("propiedadId", "caracteristicaId");

                    b.HasIndex("caracteristicaId");

                    b.HasIndex("propiedadId1");

                    b.ToTable("TipoPropiedadCaracteristica");
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoPropiedadTipoAmbiente", b =>
                {
                    b.Property<string>("tipoPropiedadTipoAmbienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("tipoAmbienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tipoPropiedadId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("tipoPropiedadTipoAmbienteId");

                    b.HasIndex("tipoAmbienteId");

                    b.HasIndex("tipoPropiedadId");

                    b.ToTable("TipoPropiedadTipoAmbiente");
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoPublicacion", b =>
                {
                    b.Property<string>("tipoPublicacionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("nombreTipoPublicacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipoPublicacionId");

                    b.ToTable("TipoPublicacion");
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoPublicante", b =>
                {
                    b.Property<string>("tipoPublicanteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("nombreTipoPublicante")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipoPublicanteId");

                    b.ToTable("TipoPublicante");
                });

            modelBuilder.Entity("API_Persistencia.Models.Usuario", b =>
                {
                    b.Property<string>("usuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Key")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Vector")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("admin")
                        .HasColumnType("bit");

                    b.Property<string>("apellidoUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("contraseña")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("emailConfirmado")
                        .HasColumnType("bit");

                    b.Property<string>("nombreUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("permiteSerNotificado")
                        .HasColumnType("bit");

                    b.Property<bool>("permiterSerContactadoPorPublicante")
                        .HasColumnType("bit");

                    b.Property<long>("telefono1")
                        .HasColumnType("bigint");

                    b.Property<long>("telefono2")
                        .HasColumnType("bigint");

                    b.HasKey("usuarioId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("API_Persistencia.Models.VisitaInmueble", b =>
                {
                    b.Property<string>("visitaInmuebleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("cantidadVecesQueRepitioVisita")
                        .HasColumnType("int");

                    b.Property<bool>("contactoPublicante")
                        .HasColumnType("bit");

                    b.Property<DateTime>("fechaHoraVisitaInmueble")
                        .HasColumnType("datetime2");

                    b.Property<string>("publicacionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("usuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("visitaInmuebleId");

                    b.HasIndex("publicacionId");

                    b.HasIndex("usuarioId");

                    b.ToTable("VisitaInmueble");
                });

            modelBuilder.Entity("API_Persistencia.Models.VisitaPerfilPublicante", b =>
                {
                    b.Property<string>("visitaPerfilPublicanteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("fechaHoraVisitaPerfilPublicante")
                        .HasColumnType("datetime2");

                    b.Property<string>("publicacionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("usuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("visitaPerfilPublicanteId");

                    b.HasIndex("publicacionId");

                    b.HasIndex("usuarioId");

                    b.ToTable("VisitaPerfilPublicante");
                });

            modelBuilder.Entity("API_Persistencia.Models.Actividad", b =>
                {
                    b.HasOne("API_Persistencia.Models.Usuario", null)
                        .WithMany("Actividad")
                        .HasForeignKey("usuarioId");
                });

            modelBuilder.Entity("API_Persistencia.Models.Credito", b =>
                {
                    b.HasOne("API_Persistencia.Models.TipoMoneda", "TipoMoneda")
                        .WithMany()
                        .HasForeignKey("TipoMonedaID");
                });

            modelBuilder.Entity("API_Persistencia.Models.Favorito", b =>
                {
                    b.HasOne("API_Persistencia.Models.Publicacion", "Publicacion")
                        .WithMany()
                        .HasForeignKey("publicacionId");

                    b.HasOne("API_Persistencia.Models.Usuario", null)
                        .WithMany("Favorito")
                        .HasForeignKey("usuarioId");
                });

            modelBuilder.Entity("API_Persistencia.Models.ImagenPropiedad", b =>
                {
                    b.HasOne("API_Persistencia.Models.Propiedad", null)
                        .WithMany("ImagenPropiedad")
                        .HasForeignKey("propiedadId");
                });

            modelBuilder.Entity("API_Persistencia.Models.PagoMP", b =>
                {
                    b.HasOne("API_Persistencia.Models.Usuario", "payer")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("API_Persistencia.Models.Plan", b =>
                {
                    b.HasOne("API_Persistencia.Models.TipoMoneda", "TipoMoneda")
                        .WithMany()
                        .HasForeignKey("tipoMonedaId");
                });

            modelBuilder.Entity("API_Persistencia.Models.PlanUsuario", b =>
                {
                    b.HasOne("API_Persistencia.Models.Credito", "Credito")
                        .WithMany()
                        .HasForeignKey("CreditoPaqueteId");

                    b.HasOne("API_Persistencia.Models.PagoMP", "Pago")
                        .WithMany()
                        .HasForeignKey("PagoMPId");

                    b.HasOne("API_Persistencia.Models.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("planId");

                    b.HasOne("API_Persistencia.Models.Usuario", null)
                        .WithMany("PlanUsuario")
                        .HasForeignKey("usuarioId");
                });

            modelBuilder.Entity("API_Persistencia.Models.Propiedad", b =>
                {
                    b.HasOne("API_Persistencia.Models.TipoConstruccion", "TipoConstruccion")
                        .WithMany()
                        .HasForeignKey("tipoConstruccionId");

                    b.HasOne("API_Persistencia.Models.TipoMoneda", "TipoMoneda")
                        .WithMany()
                        .HasForeignKey("tipoMonedaId");

                    b.HasOne("API_Persistencia.Models.TipoPropiedad", "TipoPropiedad")
                        .WithMany()
                        .HasForeignKey("tipoPropiedadId");

                    b.HasOne("API_Persistencia.Models.TipoPublicante", "TipoPublicante")
                        .WithMany()
                        .HasForeignKey("tipoPublicanteId");

                    b.HasOne("API_Persistencia.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("usuarioId");
                });

            modelBuilder.Entity("API_Persistencia.Models.PropiedadTipoAmbiente", b =>
                {
                    b.HasOne("API_Persistencia.Models.Propiedad", null)
                        .WithMany("PropiedadTipoAmbiente")
                        .HasForeignKey("propiedadId");

                    b.HasOne("API_Persistencia.Models.TipoAmbiente", "TipoAmbiente")
                        .WithMany()
                        .HasForeignKey("tipoAmbienteId");
                });

            modelBuilder.Entity("API_Persistencia.Models.Publicacion", b =>
                {
                    b.HasOne("API_Persistencia.Models.Propiedad", "Propiedad")
                        .WithMany()
                        .HasForeignKey("propiedadId");

                    b.HasOne("API_Persistencia.Models.TipoPublicacion", "TipoPublicacion")
                        .WithMany()
                        .HasForeignKey("tipoPublicacionId");
                });

            modelBuilder.Entity("API_Persistencia.Models.SolicitudContactoVisitante", b =>
                {
                    b.HasOne("API_Persistencia.Models.Publicacion", "Publicacion")
                        .WithMany()
                        .HasForeignKey("publicacionId");

                    b.HasOne("API_Persistencia.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("usuarioId");
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoPropiedadCaracteristica", b =>
                {
                    b.HasOne("API_Persistencia.Models.TipoPropiedad", "tipoPropiedad")
                        .WithMany("caracteristica")
                        .HasForeignKey("caracteristicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Persistencia.Models.Caracteristica", "caracteristicas")
                        .WithMany("tipoPropiedadCaracteristicas")
                        .HasForeignKey("propiedadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Persistencia.Models.Propiedad", "propiedad")
                        .WithMany("PropiedadCaracteristica")
                        .HasForeignKey("propiedadId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API_Persistencia.Models.TipoPropiedadTipoAmbiente", b =>
                {
                    b.HasOne("API_Persistencia.Models.TipoAmbiente", "TipoAmbiente")
                        .WithMany()
                        .HasForeignKey("tipoAmbienteId");

                    b.HasOne("API_Persistencia.Models.TipoPropiedad", null)
                        .WithMany("TipoPropiedadTipoAmbiente")
                        .HasForeignKey("tipoPropiedadId");
                });

            modelBuilder.Entity("API_Persistencia.Models.VisitaInmueble", b =>
                {
                    b.HasOne("API_Persistencia.Models.Publicacion", null)
                        .WithMany("VisitaInmueble")
                        .HasForeignKey("publicacionId");

                    b.HasOne("API_Persistencia.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("usuarioId");
                });

            modelBuilder.Entity("API_Persistencia.Models.VisitaPerfilPublicante", b =>
                {
                    b.HasOne("API_Persistencia.Models.Publicacion", null)
                        .WithMany("VisitaPerfilPublicante")
                        .HasForeignKey("publicacionId");

                    b.HasOne("API_Persistencia.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("usuarioId");
                });
#pragma warning restore 612, 618
        }
    }
}
