using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    /// <summary>
    /// Represents a favorite transfer.
    /// </summary>
    public class TransferenciaFavorito
    {
        /// <summary>
        /// The unique identifier of the favorite.
        /// </summary>
        public string favoritoId { get; set; }

        /// <summary>
        /// Indicates whether the favorite is active or not.
        /// </summary>
        public bool activo { get; set; }

        /// <summary>
        /// The unique identifier of the publication.
        /// </summary>
        public string publicacionId { get; set; }

        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public string usuarioId { get; set; }

        /// <summary>
        /// The associated publication. It can be null.
        /// </summary>
        public virtual TransferenciaPublicacion Publicacion { get; set; }
    }
}
