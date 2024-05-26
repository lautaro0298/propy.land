using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Models
{
    public class PlanUsuario
    {
        [Key]
        public string planUsuarioId { get; set; }
        public bool activo { get; set; }
        public DateTime? fechaContratacion { get; set; }
        public DateTime? fechaVencimiento { get; set; }
        public int? cantidadCreditosActivos { get; set; }
        public string planId { get; set; }
        public string usuarioId { get; set; }

        [ForeignKey("planId")]
        public virtual Plan Plan { get; set; }

        public long? NumFactura { get; set; }
        public DateTime? FechaCompra { get; set; }

        [ForeignKey("CreditoPaqueteId")]
        public virtual Credito Credito { get; set; }
        public string CreditoPaqueteId { get; set; }

        [ForeignKey("PagoMPId")]
        public virtual PagoMP Pago { get; set; }
        public string PagoMPId { get; set; }
    }

    public class Plan
    {
        [Key]
        public string planId { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
    }

    public class Credito
    {
        [Key]
        public string creditoId { get; set; }
        public string nombre { get; set; }
        public int cantidad { get; set; }
    }

    public class PagoMP
    {
        [Key]
        public string pagoMPId { get; set; }
        public string metodoPago { get; set; }
        public decimal monto { get; set; }
    }
}
