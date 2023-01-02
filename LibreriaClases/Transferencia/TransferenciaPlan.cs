using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaPlan
    {
        public string planId { get; set; }
        public string nombrePlan { get; set; }
        public bool permiteVideo { get; set; }
        public bool accesoEstadisticasAvanzadas { get; set; }
        public int cantidadCreditosIniciales { get; set; }
        public decimal precioPlan { get; set; }
        public int cantidadMaxImagenesPermitidasPorPublicacion { get; set; }
        public bool activo { get; set; }
        public string TipoMonedaID { get; set; }
        public int Vencimiento { get; set; }
        public virtual TransferenciaTipoMoneda TipoMoneda { get; set; }
    }
}
