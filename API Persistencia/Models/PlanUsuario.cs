using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class PlanUsuario
    {
        public string planUsuarioId { get; set; }
        public bool activo { get; set; }
        public DateTime fechaContratacion { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int cantidadCreditosActivos { get; set; }
        public string planId { get; set; }
        public string usuarioId { get; set; }
        public virtual Plan Plan { get; set; }
        public long NumFactura { get; set; }
        public DateTime FechaCompra { get; set; }
        public virtual Credito Credito { get; set; }
        public string CreditoPaqueteId { get; set; }
        public virtual PagoMP Pago { get; set; }
        public string PagoMPId { get; set; }
    }
}
