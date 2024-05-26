using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    /// <summary>
    /// Represents a non-persisted transfer object for creating a visitor contact request.
    /// </summary>
    public class TransferenciaNoPersistidaCrearSolicitudContactoVisitante
    {
        /// <summary>
        /// Gets or sets the transfer object for the user's plan.
        /// </summary>
        public TransferenciaPlanUsuario PlanUsuario { get; set; }

        /// <summary>
        /// Gets or sets the transfer object for the visitor contact request.
        /// </summary>
        public TransferenciaSolicitudContactoVisitante SolicitudContactoVisitante { get; set; }
    }
}
