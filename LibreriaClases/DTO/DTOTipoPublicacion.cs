using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for TipoPublicacion
    /// </summary>
    public class DTOTipoPublicacion
    {
        /// <summary>
        /// The unique identifier for the tipo publicacion
        /// </summary>
        public string TipoPublicacionId { get; set; }

        /// <summary>
        /// The name of the tipo publicacion
        /// </summary>
        public string NombreTipoPublicacion { get; set; }

        /// <summary>
        /// Initializes a new instance of the DTOTipoPublicacion class
        /// </summary>
        public DTOTipoPublicacion()
        {
            this.TipoPublicacionId = string.Empty;
            this.NombreTipoPublicacion = string.Empty;
        }
    }
}
