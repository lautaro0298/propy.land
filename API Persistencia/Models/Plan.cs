using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class Plan
    {
        public string planId { get; set; }
        public string nombrePlan { get; set; }
        public bool permiteVideo { get; set; }
        public bool accesoEstadisticasAvanzadas { get; set; }
        public int cantidadCreditosIniciales { get; set; }
        public decimal precioPlan { get; set; }
        public int cantidadMaxImagenesPermitidasPorPublicacion { get; set; }
        public bool activo { get; set; }
        public string tipoMonedaId { get; set; }
        public int Vencimiento { get; set; }
        public virtual TipoMoneda TipoMoneda { get; set; }
    }
}
