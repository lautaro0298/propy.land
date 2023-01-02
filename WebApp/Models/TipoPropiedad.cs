using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("TipoPropiedad")]
    public class TipoPropiedad
    {
        [Key]
        public Guid tipoPropiedadId { get; set; }
        [Display(Name ="Tipo Propiedad")]
        public string nombreTipoPropiedad { get; set; }
        public bool activo { get; set; }

        //Indico que un TipoPropiedad puede estar en una o muchas Propiedades
        public virtual ICollection<Propiedad> Propiedad { get; set; }
    }
}