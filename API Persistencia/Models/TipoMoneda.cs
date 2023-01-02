using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class TipoMoneda
    {
        public string tipoMonedaId { get; set; }
        public string denominacionMoneda { get; set; }
        public bool activo { get; set; }
    }
}
