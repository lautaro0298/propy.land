using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class VisitaPerfilPublicante
    {
        public string visitaPerfilPublicanteId { get; set; }
        public DateTime fechaHoraVisitaPerfilPublicante { get; set; }
        public string publicacionId { get; set; }
        public string usuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
