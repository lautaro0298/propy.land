using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class TipoPropiedadTipoAmbiente
    {
        public string tipoPropiedadTipoAmbienteId { get; set; }
        public bool activo { get; set; }
        public string tipoAmbienteId { get; set; }
        public string tipoPropiedadId { get; set; }
        public virtual TipoAmbiente TipoAmbiente { get; set; }
    }
}
