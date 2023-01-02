using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaVisitaInmueble
    {
        public string visitaInmuebleId { get; set; }
        public DateTime fechaHoraVisitaInmueble { get; set; }
        public int cantidadVecesQueRepitioVisita { get; set; }
        public bool contactoPublicante { get; set; }
        public string usuarioId { get; set; }
        public string publicacionId { get; set; }
        public virtual TransferenciaUsuario Usuario { get; set; }
    }
}
