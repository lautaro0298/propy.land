using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class TipoPropiedadCaracteristica
    {
        [Key]
        public string tipoPropiedadCaracteristicaID{ get; set; }

        public string  caracteristicaId { get; set; }

        public string TipopropiedadId { get; set; }
        
        public virtual Caracteristica?  caracteristicas { get; set; }
        public virtual TipoPropiedad? tipoPropiedad { get; set; }
     }
}
