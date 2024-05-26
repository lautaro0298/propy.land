using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    /// <summary>
    /// Represents a DTO (Data Transfer Object) for a PlanUsuario object.
    /// </summary>
    public class DTOPlanUsuario
    {
        /// <summary>
        /// The unique identifier of the plan usuario.
        /// </summary>
        public string PlanUsuarioId { get; set; }

        /// <summary>
        /// A value indicating whether the plan usuario is active or not.
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// The date and time when the plan usuario was contracted.
        /// </summary>
        public DateTime FechaContratacion { get; set; }

        /// <summary>
        /// The date and time when the plan usuario will expire.
        /// </summary>
        public DateTime FechaVencimiento { get; set; }

        /// <summary>
        /// The number of active credits associated with the plan usuario.
        /// </summary>
        public int CantidadCreditosActivos { get; set; }

        /// <summary>
        /// The unique identifier of the plan associated with the plan usuario.
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// The unique identifier of the user associated with the plan usuario.
        /// </summary>
        public string UsuarioId { get; set; }

        /// <summary>
        /// The DTO (Data Transfer Object) for the associated plan.
        /// </summary>
        public virtual DTOPlan DTOPlan { get; set; }

        /// <summary>
        /// The number of the invoice associated with the plan usuario.
        /// </summary>
        public long? NumFactura { get; set; }

        /// <summary>
        /// The date and time when the plan usuario was purchased.
        /// </summary>
        public DateTime? FechaCompra { get; set; }

        /// <summary>
        /// The DTO (Data Transfer Object) for the associated credit.
        /// </summary>
        public virtual DTOCredito DTOCredito { get; set; }

        /// <summary>
        /// The unique identifier of the credit package associated with the plan usuario.
        /// </summary>
        public string CreditoPaqueteId { get; set; }

        /// <summary>
        /// The DTO (Data Transfer Object) for the associated payment.
        /// </summary>
        public virtual DTOPagoMP DTOPagoMP { get; set; }

        /// <summary>
        /// The unique identifier of the payment associated with the plan usuario.
        /// </summary>
        public string PagoMPId { get; set; }

        /// <summary>
        /// Initializes a new instance of the DTOPlanUsuario class.
        /// </summary>
        public DTOPlanUsuario()
        {
            PlanUsuarioId = string.Empty;
            Activo = false;
            FechaContratacion = DateTime.MinValue;
            FechaVencimiento = DateTime.MinValue;
            CantidadCreditosActivos = 0;
            PlanId = string.Empty;
            UsuarioId = string.Empty;
            NumFactura = null;
            FechaCompra = null;
            CreditoPaqueteId = string.Empty;
            PagoMPId = string.Empty;
        }
    }
}
