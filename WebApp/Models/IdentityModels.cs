using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;

namespace WebApp.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(256)]
        public string nombre { get; set; }

        [Required]
        [StringLength(256)]
        public string apellido { get; set; }

        public long nroTelefono { get; set; }

        public bool permitirSerContactadoPublicante { get; set; }

        public bool permitirSerNotificado { get; set; }

        //Indico que un Usuario puede tener una o muchas Publicaciones
        public virtual ICollection<Publicacion> Publicacion { get; set; }
        //Indico que un Usuario puede tener una o muchas Propiedades
        public virtual ICollection<Propiedad> Propiedad { get; set; }
        
        public virtual ICollection<ActividadUsuario> ActividadUsuario { get; set; }

        //Indico que un Usuario puede tener una o muchas Compras del plan
        public virtual ICollection<HistorialCompraPlan> HistorialCompraPlan { get; set; }
        //Indico que un Usuario puede tener una o muchas Compras de créditos
        public virtual ICollection<HistorialCompraCredito> HistorialCompraCredito { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            
            return new ApplicationDbContext();
        }

        public DbSet<Publicacion> Publicacion { get; set; }
        public DbSet<TipoPublicacion> TipoPublicacion { get; set; }
        public DbSet<Propiedad> Propiedad { get; set; }
        public DbSet<TipoPropiedad> TipoPropiedad { get; set; }
        public DbSet<TipoAmbiente> TipoAmbiente { get; set; }
        public DbSet<PropiedadTipoAmbiente> PropiedadTipoAmbiente { get; set; }
        public DbSet<TipoUser> TipoUser { get; set; }
        
        public DbSet<TipoConstruccion> TipoConstruccion { get; set; }
        public DbSet<Extras> Extras { get; set; }
        public DbSet<PropiedadExtras> PropiedadExtras { get; set; }
        public DbSet<PublicacionEstado> PublicacionEstado { get; set; }
        public DbSet<EstadoPublicacion> EstadoPublicacion { get; set; }
        public DbSet<ClickPublicacion> ClickPublicacion { get; set; }
        public DbSet<TipoMoneda> TipoMoneda { get; set; }
        public DbSet<ImagenPublicacion> ImagenPublicacion { get; set; }
        public DbSet<ClickContactoPublicante> ClickContactoPublicante { get; set; }

        public DbSet<Cotizacion> Cotizacion { get; set; }

        public DbSet<Plan> Plan { get; set; }
        public DbSet<Favorito> Favorito { get; set; }
        public DbSet<PlanUsuario> PlanUsuario { get; set; }
        public DbSet<CreditoPlan> creditoPlan { get; set; }
        public DbSet<ParametrizacionDescuentoCredito> parametrizacionDescuentoCredito { get; set; }

        public DbSet<ActividadUsuario> ActividadUsuario { get; set; }
        public DbSet<HistorialCompraCredito> historialCompraCredito { get; set; }

        public DbSet<HistorialCompraPlan> historialCompraPlan { get; set; }
    }
}