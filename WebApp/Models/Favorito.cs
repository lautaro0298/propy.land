using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("Favorito")]
    public class Favorito
    {
        [Key]
        public string favoritoId { get; set; }
        public bool activo { get; set; }
   
        
        public string usuarioId { get; set; }
        [ForeignKey("usuarioId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public Guid publicacionId { get; set; }
        [ForeignKey("publicacionId")]
        public virtual Publicacion Publicacion { get; set; }
    }
}