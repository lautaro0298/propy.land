using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class TipoPublicacion
    {
        public string tipoPublicacionId { get; set; }
        public string nombreTipoPublicacion { get; set; }
        public bool activo { get; set; }
    }
}
