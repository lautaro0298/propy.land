using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Models
{
    [Table("TipoConstruccion")]
    public class TipoConstruccion
    {
        [Key]
        public Guid tipoConstuccionId { get; set; }
        public string nombreTipoConstruccion { get; set; }
        public bool activo { get; set; }

        //Indico que un TipoConstruccion puede estar en una o muchas Propiedades
        public virtual ICollection<Propiedad> Propiedad { get; set; }
    }
}