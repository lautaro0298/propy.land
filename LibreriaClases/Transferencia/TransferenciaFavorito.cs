using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaFavorito
    {
        public string favoritoId { get; set; }
        public bool activo { get; set; }
        public string publicacionId { get; set; }
        public string usuarioId { get; set; }
        public virtual TransferenciaPublicacion Publicacion { get; set; }
    }
}
