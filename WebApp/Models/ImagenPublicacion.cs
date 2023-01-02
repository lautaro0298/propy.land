using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("ImagenPublicacion")]
    public class ImagenPublicacion
    {
        [Key]
        public Guid imagenPublicacionId { get; set; }

        public bool imagenRepresentativa { get; set; }
        [ForeignKey("Publicacion")]
        public Guid publicacionId { get; set; }
        public virtual Publicacion Publicacion { get; set; }

        public string rutaBD { get; set; }
    }
}