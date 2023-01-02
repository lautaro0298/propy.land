using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class TipoConstruccion
    {
        public string tipoConstruccionId { get; set; }
        public string nombreTipoConstruccion { get; set; }
        public bool activo { get; set; }
    }
}
