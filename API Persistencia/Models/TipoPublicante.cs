using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class TipoPublicante
    {
        public string tipoPublicanteId { get; set; }
        public string nombreTipoPublicante { get; set; }
        public bool activo { get; set; }
    }
}
