using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("TipoPublicacion")]
    public class TipoPublicacion
    {
        [Key]
        public Guid tipoPublicacionId { get; set; }
        [Display(Name ="Tipo Publicación")]
        public string nombreTipoPublicacion { get; set; }
        public bool activo { get; set; }

        //Indico que un TipoPublicacion puede estar en una o muchas Publicaciones
        public virtual ICollection<Publicacion> Publicacion { get; set; }
    }
}