using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for TipoPublicante
    /// </summary>
    public class DTOTipoPublicante
    {
        /// <summary>
        /// The unique identifier for the tipo publicante
        /// </summary>
        public string TipoPublicanteId { get; set; }

        /// <summary>
        /// The name of the tipo publicante
        /// </summary>
        public string NombreTipoPublicante { get; set; }

        /// <summary>
        /// Initializes a new instance of the DTOTipoPublicante class
        /// </summary>
        public DTOTipoPublicante()
        {
        }

        /// <summary>
        /// Initializes a new instance of the DTOTipoPublicante class
        /// with the specified tipo publicante id and name
        /// </summary>
        /// <param name="tipoPublicanteId">The unique identifier for the tipo publicante</param>
        /// <param name="nombreTipoPublicante">The name of the tipo publicante</param>
        public DTOTipoPublicante(string tipoPublicanteId, string nombreTipoPublicante)
        {
            TipoPublicanteId = tipoPublicanteId;
            NombreTipoPublicante = nombreTipoPublicante;
        }
    }
}
