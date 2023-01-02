using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class VisitaInmueble
    {
        public string visitaInmuebleId { get; set; }
        public DateTime fechaHoraVisitaInmueble { get; set; }
        public int cantidadVecesQueRepitioVisita { get; set; }
        public bool contactoPublicante { get; set; }
        public string usuarioId { get; set; }
        public string publicacionId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
