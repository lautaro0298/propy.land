using System;
using System.Linq;
using API_Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    [DbContext(typeof(ConexionDB))]
    [Migration("20220827234127_v1.45")]
    partial class v145
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Actividad>(b =>
            {
                b.Property(e => e.actividadId).HasColumnType("nvarchar(450)");
                b.Property(e => e.descripcionActividad).HasColumnType("nvarchar(max)");
                b.Property(e => e.fechaActividad).HasColumnType("datetime2");
                b.Property(e => e.usuarioId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.actividadId);

                b.HasIndex(e => e.usuarioId);
            });

            modelBuilder.Entity<Caracteristica>(b =>
            {
                b.Property(e => e.caracteristicaId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.nombreCaracteristica).HasColumnType("nvarchar(max)");

                b.HasKey(e => e.caracteristicaId);
            });

            modelBuilder.Entity<Credito>(b =>
            {
                b.Property(e => e.PaqueteID).HasColumnType("nvarchar(450)");
                b.Property(e => e.Activo).HasColumnType("bit");
                b.Property(e => e.CantidadCreditos).HasColumnType("int");
                b.Property(e => e.NombrePack).HasColumnType("nvarchar(max)");
                b.Property(e => e.Precio).HasColumnType("decimal(18,2)");

                b.Property(e => e.TipoMonedaID).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.PaqueteID);

                b.HasIndex(e => e.TipoMonedaID);
            });

            modelBuilder.Entity<Favorito>(b =>
            {
                b.Property(e => e.favoritoId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.publicacionId).HasColumnType("nvarchar(450)");
                b.Property(e => e.usuarioId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.favoritoId);

                b.HasIndex(e => e.publicacionId);
                b.HasIndex(e => e.usuarioId);
            });

            modelBuilder.Entity<ImagenPropiedad>(b =>
            {
                b.Property(e => e.ImagenPropiedadId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.propiedadId).HasColumnType("nvarchar(450)");
                b.Property(e => e.rutaImagenPropiedad).HasColumnType("nvarchar(max)");

                b.HasKey(e => e.ImagenPropiedadId);

                b.HasIndex(e => e.propiedadId);
            });

            modelBuilder.Entity<PagoMP>(b =>
            {
                b.Property(e => e.PagoMPId).HasColumnType("nvarchar(450)");
                b.Property(e => e.UsuarioId).HasColumnType("nvarchar(450)");
                b.Property(e => e.currency_id).HasColumnType("nvarchar(max)");
                b.Property(e => e.date_approved).HasColumnType("datetime2");
                b.Property(e => e.date_created).HasColumnType("datetime2");
                b.Property(e => e.idPago).HasColumnType("nvarchar(max)");
                b.Property(e => e.operation_type).HasColumnType("nvarchar(max)");
                b.Property(e => e.payment_type_id).HasColumnType("nvarchar(max)");
                b.Property(e => e.status).HasColumnType("nvarchar(max)");
                b.Property(e => e.transaction_amount).HasColumnType("real");

                b.HasKey(e => e.PagoMPId);

                b.HasIndex(e => e.UsuarioId);
            });

            modelBuilder.Entity<Plan>(b =>
            {
                b.Property(e => e.planId).HasColumnType("nvarchar(450)");
                b.Property(e => e.Vencimiento).HasColumnType("int");
                b.Property(e => e.accesoEstadisticasAvanzadas).HasColumnType("bit");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.cantidadCreditosIniciales).HasColumnType("int");
                b.Property(e => e.cantidadMaxImagenesPermitidasPorPublicacion).HasColumnType("int");
                b.Property(e => e.nombrePlan).HasColumnType("nvarchar(max)");
                b.Property(e => e.permiteVideo).HasColumnType("bit");
                b.Property(e => e.precioPlan).HasColumnType("decimal(18,2)");

                b.Property(e => e.tipoMonedaId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.planId);

                b.HasIndex(e => e.tipoMonedaId);
            });

            modelBuilder.Entity<PlanUsuario>(b =>
            {
                b.Property(e => e.planUsuarioId).HasColumnType("nvarchar(450)");
                b.Property(e => e.CreditoPaqueteId).HasColumnType("nvarchar(450)");
                b.Property(e => e.FechaCompra).HasColumnType("datetime2");
                b.Property(e => e.NumFactura).HasColumnType("bigint");
                b.Property(e => e.PagoMPId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.cantidadCreditosActivos).HasColumnType("int");
                b.Property(e => e.fechaContratacion).HasColumnType("datetime2");
                b.Property(e => e.fechaVencimiento).HasColumnType("datetime2");
                b.Property(e => e.planId).HasColumnType("nvarchar(450)");
                b.Property(e => e.usuarioId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.planUsuarioId);

                b.HasIndex(e => e.CreditoPaqueteId);
                b.HasIndex(e => e.PagoMPId);
                b.HasIndex(e => e.planId);
                b.HasIndex(e => e.usuarioId);
            });

            modelBuilder.Entity<Propiedad>(b =>
            {
                b.Property(e => e.propiedadId).HasColumnType("nvarchar(450)");
                b.Property(e => e.AreaAdmNivel1).HasColumnType("nvarchar(max)");
                b.Property(e => e.AreaAdmNivel2).HasColumnType("nvarchar(max)");
                b.Property(e => e.amueblado).HasColumnType("bit");
                b.Property(e => e.añosAntiguedad).HasColumnType("int");
                b.Property(e => e.descripcionPropiedad).HasColumnType("nvarchar(max)");
                b.Property(e => e.fechaRegistro).HasColumnType("datetime2");
                b.Property(e => e.importeExpensasUltimoMes).HasColumnType("decimal(18,2)");
                b.Property(e => e.latitud).HasColumnType("float");
                b.Property(e => e.longitud).HasColumnType("float");
                b.Property(e => e.nroPisos).HasColumnType("int");
                b.Property(e => e.pais).HasColumnType("nvarchar(max)");
                b.Property(e => e.precioPropiedad).HasColumnType("decimal(18,2)");
                b.Property(e => e.superficieCubierta).HasColumnType("int");
                b.Property(e => e.superficieTerreno).HasColumnType("int");

                b.Property(e => e.tipoConstruccionId).HasColumnType("nvarchar(450)");
                b.Property(e => e.tipoMonedaId).HasColumnType("nvarchar(450)");
                b.Property(e => e.tipoPropiedadId).HasColumnType("nvarchar(max)");
                b.Property(e => e.tipoPublicanteId).HasColumnType("nvarchar(450)");
                b.Property(e => e.ubicacion).HasColumnType("nvarchar(max)");
                b.Property(e => e.usuarioId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.propiedadId);

                b.HasIndex(e => e.tipoConstruccionId);
                b.HasIndex(e => e.tipoMonedaId);
                b.HasIndex(e => e.tipoPublicanteId);
                b.HasIndex(e => e.usuarioId);
            });

            modelBuilder.Entity<PropiedadTipoAmbiente>(b =>
            {
                b.Property(e => e.propiedadTipoAmbienteId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.cantidad).HasColumnType("int");

                b.Property(e => e.propiedadId).HasColumnType("nvarchar(450)");
                b.Property(e => e.tipoAmbienteId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.propiedadTipoAmbienteId);

                b.HasIndex(e => e.propiedadId);
                b.HasIndex(e => e.tipoAmbienteId);
            });

            modelBuilder.Entity<Publicacion>(b =>
            {
                b.Property(e => e.publicacionId).HasColumnType("nvarchar(450)");
                b.Property(e => e.estado).HasColumnType("int");
                b.Property(e => e.fechaFinPublicacion).HasColumnType("datetime2");
                b.Property(e => e.fechaInicioPublicacion).HasColumnType("datetime2");

                b.Property(e => e.propiedadId).HasColumnType("nvarchar(450)");
                b.Property(e => e.tipoPublicacionId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.publicacionId);

                b.HasIndex(e => e.propiedadId);
                b.HasIndex(e => e.tipoPublicacionId);
            });

            modelBuilder.Entity<SolicitudContactoVisitante>(b =>
            {
                b.Property(e => e.solicitudContactoVisitanteId).HasColumnType("nvarchar(450)");
                b.Property(e => e.cantidadVecesRealizoSolicitud).HasColumnType("int");
                b.Property(e => e.fechaSolicitud).HasColumnType("datetime2");

                b.Property(e => e.publicacionId).HasColumnType("nvarchar(450)");
                b.Property(e => e.usuarioId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.solicitudContactoVisitanteId);

                b.HasIndex(e => e.publicacionId);
                b.HasIndex(e => e.usuarioId);
            });

            modelBuilder.Entity<TipoAmbiente>(b =>
            {
                b.Property(e => e.tipoAmbienteId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.nombreTipoAmbiente).HasColumnType("nvarchar(max)");

                b.HasKey(e => e.tipoAmbienteId);
            });

            modelBuilder.Entity<TipoConstruccion>(b =>
            {
                b.Property(e => e.tipoConstruccionId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.nombreTipoConstruccion).HasColumnType("nvarchar(max)");

                b.HasKey(e => e.tipoConstruccionId);
            });

            modelBuilder.Entity<TipoMoneda>(b =>
            {
                b.Property(e => e.tipoMonedaId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.denominacionMoneda).HasColumnType("nvarchar(max)");

                b.HasKey(e => e.tipoMonedaId);
            });

            modelBuilder.Entity<TipoPropiedad>(b =>
            {
                b.Property(e => e.tipoPropiedadId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.nombreTipoPropiedad).HasColumnType("nvarchar(max)");

                b.HasKey(e => e.tipoPropiedadId);
            });

            modelBuilder.Entity<TipoPropiedadCaracteristica>(b =>
            {
                b.Property(e => e.propiedadId).HasColumnType("nvarchar(450)");
                b.Property(e => e.caracteristicaId).HasColumnType("nvarchar(450)");
                b.Property(e => e.tipoPropiedadCaracteristicaID).HasColumnType("nvarchar(max)");

                b.HasKey(e => new { e.propiedadId, e.caracteristicaId });

                b.HasIndex(e => e.caracteristicaId);
                b.HasIndex(e => e.propiedadId);
            });

            modelBuilder.Entity<TipoPropiedadTipoAmbiente>(b =>
            {
                b.Property(e => e.tipoPropiedadTipoAmbienteId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");

                b.Property(e => e.tipoAmbienteId).HasColumnType("nvarchar(450)");
                b.Property(e => e.tipoPropiedadId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.tipoPropiedadTipoAmbienteId);

                b.HasIndex(e => e.tipoAmbienteId);
                b.HasIndex(e => e.tipoPropiedadId);
            });

            modelBuilder.Entity<TipoPublicacion>(b =>
            {
                b.Property(e => e.tipoPublicacionId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.nombreTipoPublicacion).HasColumnType("nvarchar(max)");

                b.HasKey(e => e.tipoPublicacionId);
            });

            modelBuilder.Entity<TipoPublicante>(b =>
            {
                b.Property(e => e.tipoPublicanteId).HasColumnType("nvarchar(450)");
                b.Property(e => e.activo).HasColumnType("bit");
                b.Property(e => e.nombreTipoPublicante).HasColumnType("nvarchar(max)");

                b.HasKey(e => e.tipoPublicanteId);
            });

            modelBuilder.Entity<Usuario>(b =>
            {
                b.Property(e => e.usuarioId).HasColumnType("nvarchar(450)");
                b.Property(e => e.Key).HasColumnType("varbinary(max)");
                b.Property(e => e.Vector).HasColumnType("varbinary(max)");
                b.Property(e => e.admin).HasColumnType("bit");
                b.Property(e => e.apellidoUsuario).HasColumnType("nvarchar(max)");
                b.Property(e => e.contraseña).HasColumnType("varbinary(max)");
                b.Property(e => e.email).HasColumnType("nvarchar(max)");
                b.Property(e => e.emailConfirmado).HasColumnType("bit");
                b.Property(e => e.nombreUsuario).HasColumnType("nvarchar(max)");
                b.Property(e => e.permiteSerNotificado).HasColumnType("bit");
                b.Property(e => e.permiterSerContactadoPorPublicante).HasColumnType("bit");
                b.Property(e => e.telefono1).HasColumnType("bigint");
                b.Property(e => e.telefono2).HasColumnType("bigint");

                b.HasKey(e => e.usuarioId);
            });

            modelBuilder.Entity<VisitaInmueble>(b =>
            {
                b.Property(e => e.visitaInmuebleId).HasColumnType("nvarchar(450)");
                b.Property(e => e.cantidadVecesQueRepitioVisita).HasColumnType("int");
                b.Property(e => e.contactoPublicante).HasColumnType("bit");
                b.Property(e => e.fechaHoraVisitaInmueble).HasColumnType("datetime2");

                b.Property(e => e.publicacionId).HasColumnType("nvarchar(450)");
                b.Property(e => e.usuarioId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.visitaInmuebleId);

                b.HasIndex(e => e.publicacionId);
                b.HasIndex(e => e.usuarioId);
            });

            modelBuilder.Entity<VisitaPerfilPublicante>(b =>
            {
                b.Property(e => e.visitaPerfilPublicanteId).HasColumnType("nvarchar(450)");
                b.Property(e => e.fechaHoraVisitaPerfilPublicante).HasColumnType("datetime2");

                b.Property(e => e.publicacionId).HasColumnType("nvarchar(450)");
                b.Property(e => e.usuarioId).HasColumnType("nvarchar(450)");

                b.HasKey(e => e.visitaPerfilPublicanteId);

                b.HasIndex(e => e.publicacionId);
                b.HasIndex(e => e.usuarioId);
            });

            modelBuilder.Entity<Actividad>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Actividad)
                .HasForeignKey(a => a.usuarioId);

            modelBuilder.Entity<Credito>()
                .HasOne(c => c.TipoMoneda)
                .WithMany()
                .HasForeignKey(c => c.TipoMonedaID);

            modelBuilder.Entity<Favorito>()
                .HasOne(f => f.Publicacion)
                .WithMany()
                .HasForeignKey(f => f.publicacionId);

            modelBuilder.Entity<Favorito>()
                .HasOne(f => f.Usuario)
                .WithMany(u => u.Favorito)
                .HasForeignKey(f => f.usuarioId);

            modelBuilder.Entity<ImagenPropiedad>()
                .HasOne(i => i.Propiedad)
                .WithMany(p => p.ImagenPropiedad)
                .HasForeignKey(i => i.propiedadId);

            modelBuilder.Entity<PagoMP>()
                .HasOne(p => p.payer)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<Plan>()
                .HasOne(p => p.TipoMoneda)
                .WithMany()
                .HasForeignKey(p => p.tipoMonedaId);

            modelBuilder.Entity<PlanUsuario>()
                .HasOne(pu => pu.Credito)
                .WithMany()
                .HasForeignKey(pu => pu.CreditoPaqueteId);

            modelBuilder.Entity<PlanUsuario>()
                .HasOne(pu => pu.Pago)
                .WithMany()
                .HasForeignKey(pu => pu.PagoMPId);

            modelBuilder.Entity<PlanUsuario>()
                .HasOne(pu => pu.Plan)
                .WithMany()
                .HasForeignKey(pu => pu.planId);

            modelBuilder.Entity<PlanUsuario>()
                .HasOne(pu => pu.Usuario)
                .WithMany(u => u.PlanUsuario)
                .HasForeignKey(pu => pu.usuarioId);

            modelBuilder.Entity<Propiedad>()
                .HasOne(p => p.TipoConstruccion)
                .WithMany()
                .HasForeignKey(p => p.tipoConstruccionId);

            modelBuilder.Entity<Propiedad>()
                .HasOne(p => p.TipoMoneda)
                .WithMany()
                .HasForeignKey(p => p.tipoMonedaId);

            modelBuilder.Entity<Propiedad>()
                .HasOne(p => p.TipoPublicante)
                .WithMany()
                .HasForeignKey(p => p.tipoPublicanteId);

            modelBuilder.Entity<Propiedad>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.usuarioId);

            modelBuilder.Entity<PropiedadTipoAmbiente>()
                .HasOne(pta => pta.Propiedad)
                .WithMany(p => p.PropiedadTipoAmbiente)
                .HasForeignKey(pta => pta.propiedadId);

            modelBuilder.Entity<PropiedadTipoAmbiente>()
                .HasOne(pta => pta.TipoAmbiente)
                .WithMany()
                .HasForeignKey(pta => pta.tipoAmbienteId);

            modelBuilder.Entity<Publicacion>()
                .HasOne(p => p.Propiedad)
                .WithMany()
                .HasForeignKey(p => p.propiedadId);

            modelBuilder.Entity<Publicacion>()
                .HasOne(p => p.TipoPublicacion)
                .WithMany()
                .HasForeignKey(p => p.tipoPublicacionId);

            modelBuilder.Entity<SolicitudContactoVisitante>()
                .HasOne(scv => scv.Publicacion)
                .WithMany()
                .HasForeignKey(scv => scv.publicacionId);

            modelBuilder.Entity<SolicitudContactoVisitante>()
                .HasOne(scv => scv.Usuario)
                .WithMany()
                .HasForeignKey(scv => scv.usuarioId);

            modelBuilder.Entity<TipoPropiedadCaracteristica>()
                .HasOne(tpc => tpc.tipoPropiedad)
                .WithMany(tp => tp.caracteristica)
                .HasForeignKey(tpc => tpc.caracteristicaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TipoPropiedadCaracteristica>()
                .HasOne(tpc => tpc.caracteristicas)
                .WithMany(c => c.tipoPropiedadCaracteristicas)
                .HasForeignKey(tpc => tpc.propiedadId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TipoPropiedadCaracteristica>()
                .HasOne(tpc => tpc.Propiedad)
                .WithMany(p => p.PropiedadCaracteristica)
                .HasForeignKey(tpc => tpc.propiedadId1)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TipoPropiedadTipoAmbiente>()
                .HasOne(tpta => tpta.TipoAmbiente)
                .WithMany()
                .HasForeignKey(tpta => tpta.tipoAmbienteId);

            modelBuilder.Entity<TipoPropiedadTipoAmbiente>()
                .HasOne(tpta => tpta.TipoPropiedad)
                .WithMany()
                .HasForeignKey(tpta => tpta.tipoPropiedadId);

            modelBuilder.Entity<VisitaInmueble>()
                .HasOne(vi => vi.Publicacion)
                .WithMany(p => p.VisitaInmueble)
                .HasForeignKey(vi => vi.publicacionId);

            modelBuilder.Entity<VisitaInmueble>()
                .HasOne(vi => vi.Usuario)
                .WithMany()
                .HasForeignKey(vi => vi.usuarioId);

            modelBuilder.Entity<VisitaPerfilPublicante>()
                .HasOne(vpp => vpp.Publicacion)
                .WithMany(p => p.VisitaPerfilPublicante)
                .HasForeignKey(vpp => vpp.publicacionId);

            modelBuilder.Entity<VisitaPerfilPublicante>()
                .HasOne(vpp => vpp.Usuario)
                .WithMany()
                .HasForeignKey(vpp => vpp.usuarioId);
        }
    }
}
