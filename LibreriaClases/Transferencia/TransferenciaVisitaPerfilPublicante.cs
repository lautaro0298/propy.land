using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaVisitaPerfilPublicante
    {
        public string visitaPerfilPublicanteId { get; set; }
        public DateTime fechaHoraVisitaPerfilPublicante { get; set; }
        public string publicacionId { get; set; }
        public string usuarioId { get; set; }
        public virtual TransferenciaUsuario Usuario { get; set; }
    }
}
