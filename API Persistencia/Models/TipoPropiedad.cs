using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class TipoPropiedad
    {
        public TipoPropiedad()
        {
            caracteristica = new List<TipoPropiedadCaracteristica>();
        }

        public int index { get; set; }
        public string tipoPropiedadId { get; set; }
        public string nombreTipoPropiedad { get; set; }
        public bool activo { get; set; }
        public virtual List<TipoPropiedadCaracteristica> caracteristica { get; set; }
        public virtual List<TipoPropiedadTipoAmbiente> TipoPropiedadTipoAmbiente { get; set; }
        // public virtual List<Caracteristica> Caracteristicas { get; set; }

    }
}
