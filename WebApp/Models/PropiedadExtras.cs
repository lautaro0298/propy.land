using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PropiedadExtras
    {
        [Key]
        public Guid propiedadExtrasId { get; set; }

        [ForeignKey("Propiedad")]
        public Guid propiedadId { get; set; }
        public virtual Propiedad Propiedad { get; set; }

        [ForeignKey("Extras")]
        public Guid extrasId { get; set; }
        public virtual Extras Extras { get; set; }

        public bool activo { get; set; }
    }
}