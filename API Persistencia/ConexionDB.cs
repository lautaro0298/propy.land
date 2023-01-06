using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Persistencia.Models;

namespace API_Persistencia
{
    public class ConexionDB:DbContext
    {
        public ConexionDB()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoPropiedadCaracteristica>().HasKey(sc => new { sc.TipopropiedadId, sc.caracteristicaId });
            modelBuilder.Entity<TipoPropiedadCaracteristica>().HasOne(x => x.tipoPropiedad).WithMany(y => y.caracteristica).HasForeignKey(x => x.caracteristicaId);
            modelBuilder.Entity<TipoPropiedadCaracteristica>().HasOne(x=>x.caracteristicas).WithMany(y=>y.tipoPropiedadCaracteristicas).HasForeignKey(x=>x.TipopropiedadId);
            modelBuilder.Entity<PublicacionCaracteristica>().HasKey(sc => new { sc.PublicacionId, sc.CaracteristicaId });


        }

         
        public ConexionDB(DbContextOptions<ConexionDB> options)
      : base(options)
        {
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Caracteristica> Caracteristica { get; set; }
        public DbSet<Actividad> Actividad { get; set; }
        public DbSet<ImagenPropiedad> ImagenPropiedad { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<PlanUsuario> PlanUsuario { get; set; }
        public DbSet<Propiedad> Propiedad { get; set; }
   
        public DbSet<PropiedadTipoAmbiente> PropiedadTipoAmbiente { get; set; }
        public DbSet<Publicacion> Publicacion { get; set; }
        public DbSet<PublicacionCaracteristica> PublicacionCaracteristicas { get; set; }
        public DbSet<TipoAmbiente> TipoAmbiente { get; set; }
        public DbSet<TipoConstruccion> TipoConstruccion { get; set; }
        public DbSet<TipoPropiedad> TipoPropiedad { get; set; }
        public DbSet<TipoPropiedadTipoAmbiente> TipoPropiedadTipoAmbiente { get; set; }
        public DbSet<TipoPropiedadCaracteristica> TipoPropiedadCaracteristica { get; set; }
        public DbSet<TipoPublicacion> TipoPublicacion { get; set; }
        public DbSet<TipoPublicante> TipoPublicante { get; set; }
        public DbSet<VisitaInmueble> VisitaInmueble { get; set; }
        public DbSet<VisitaPerfilPublicante> VisitaPerfilPublicante { get; set; }
        public DbSet<TipoMoneda> TipoMoneda { get; set; }
        public DbSet<Credito> Credito { get; set; }
        public DbSet<SolicitudContactoVisitante> SolicitudContactoVisitante { get; set; }
        public DbSet<Favorito> Favorito { get; set; }
        public DbSet<PagoMP> PagoMP { get; set; }
    }
}
