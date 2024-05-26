using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    /// <summary>
    /// Represents the type of publisher.
    /// </summary>
    public class TransferenciaTipoPublicante
    {
        /// <summary>
        /// The unique identifier of the publisher type.
        /// </summary>
        public string TipoPublicanteId { get; set; }

        /// <summary>
        /// The name of the publisher type.
        /// </summary>
        public string NombreTipoPublicante { get; set; }

        /// <summary>
        /// A value indicating whether the publisher type is active.
        /// </summary>
        public bool Activo { get; set; }
    }
}
