using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("PublicacionEstado")]
    public class PublicacionEstado
    {
        [Key]
        public Guid publicacionEstadoId { get; set; }

        [Required]
        public DateTime fechaDesde { get; set; }
        [Required]
        public DateTime fechaHasta { get; set; }

        //Claves Foraneas
        //Clave Externa Publicacion
        [ForeignKey("Publicacion")]
        public Guid publicacionId { get; set; }
        public virtual Publicacion Publicacion { get; set; }

        //Clave Externa EstadoPublicacion
        [ForeignKey("EstadoPublicacion")]
        public Guid estadoPublicacionId { get; set; }
        public virtual EstadoPublicacion EstadoPublicacion { get; set; }
    }
}