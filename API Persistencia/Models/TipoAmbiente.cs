using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class TipoAmbiente
    {
        public string tipoAmbienteId { get; set; }
        public string nombreTipoAmbiente { get; set; }
        public bool activo { get; set; }
    }
}
