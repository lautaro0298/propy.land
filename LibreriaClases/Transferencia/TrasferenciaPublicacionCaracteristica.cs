using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaPublicacionCaracteristica
    {
        public string PublicacionCaracteristicaId { get; set; }
        public string CaracteristicaId { get; set; }
        public string PublicacionId { get; set; }
        public virtual TransferenciaPublicacion Publicacion { get; set; }
        public virtual TransferenciaCaracteristica Caracteristica { get; set; }
    }
}
