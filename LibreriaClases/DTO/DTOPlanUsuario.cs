using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOPlanUsuario
    {
        public string planUsuarioId { get; set; }
        public bool activo { get; set; }
        public DateTime fechaContratacion { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int cantidadCreditosActivos { get; set; }
        public string planId { get; set; }
        public string usuarioId { get; set; }
        public virtual DTOPlan DTOPlan { get; set; }
        public long NumFactura { get; set; }
        public DateTime FechaCompra { get; set; }
        public virtual DTOCredito DTOCredito { get; set; }
        public string CreditoPaqueteId { get; set; }
        public virtual DTOPagoMP DTOPagoMP { get; set; }
        public string PagoMPId { get; set; }
    }
}
