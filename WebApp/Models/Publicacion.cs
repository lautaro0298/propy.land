using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("Publicacion")]
    public class Publicacion { 
        [Key]
        [Required]
        public Guid publicacionId { get; set; }
        [Display(Name ="Fecha Inicio Publicación")]
        public DateTime fechaInicioPublicacion { get; set; }
        [Display(Name ="Fecha Fin Publicación")]
        public DateTime fechaFinPublicacion { get; set; }
        

        [StringLength(256)]
        [Display(Name ="Observaciones")]
        public string observaciones { get; set; }

        
        [Required]
        [Display(Name ="Precio de Propiedad")]
        public decimal precioPropiedad { get; set; }

        //Propiedad de navegacion hacia ImagenPublicacion
        public virtual ICollection<ImagenPublicacion> ImagenPublicacion { get; set; }

        //Clave Externa
        [ForeignKey("TipoMoneda")]
        public Guid tipoMonedaId { get; set; }
        public virtual TipoMoneda TipoMoneda { get; set; }

        //Clave Externa
        [ForeignKey("Propiedad")]
        public Guid propiedadId { get; set; }
        public virtual Propiedad Propiedad { get; set; }

        //Clave Externa
        [ForeignKey("TipoUser")]
        public Guid tipoUserId { get; set; }
        public virtual TipoUser TipoUser { get; set; }

        //Clave Externa
        [ForeignKey("TipoPublicacion")]
        public Guid tipoPublicacionId { get; set; }
        public virtual TipoPublicacion TipoPublicacion { get; set; }

        //Clave Externa Usuario
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //Indico que una Publicacion puede tener una o muchas PublicacionEstado
        public virtual ICollection<PublicacionEstado> PublicacionEstado { get; set; }

    }
}