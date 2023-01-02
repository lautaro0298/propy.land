using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("TipoUser")]
    public class TipoUser
    {
        [Key]
        public Guid tipoUserId { get; set; }
        public string nombreTipoUser { get; set; }
        public bool activo { get; set; }

        //Indico que una TipoUser puede estar en una o muchas Publicaciones
        public virtual ICollection<Publicacion> Publicacion { get; set; }
    }
}