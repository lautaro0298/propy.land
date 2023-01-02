using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class Caracteristica
    {
        public Caracteristica() { tipoPropiedadCaracteristicas = new List<TipoPropiedadCaracteristica>(); }
        public string caracteristicaId { get; set; }
        public string nombreCaracteristica { get; set; }
        public bool activo { get; set; }
        //public virtual List<TipoPropiedad> propiedad { get; set; }  
        public virtual ICollection<PublicacionCaracteristica> PublicacionCaracteristicas { get; set; }
        public virtual List<TipoPropiedadCaracteristica> tipoPropiedadCaracteristicas { get; set; }
    }
}
