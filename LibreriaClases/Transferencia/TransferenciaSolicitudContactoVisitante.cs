using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaSolicitudContactoVisitante
    {
        public string solicitudContactoVisitanteId { get; set; }
        public int cantidadVecesRealizoSolicitud { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public string publicacionId { get; set; }
        public string usuarioId { get; set; }
        public virtual TransferenciaUsuario Usuario { get; set; }
        public virtual TransferenciaPublicacion Publicacion { get; set; }
    }
}
