using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class SolicitudContactoVisitante
    {
        public string solicitudContactoVisitanteId { get; set; }
        public int cantidadVecesRealizoSolicitud { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public string publicacionId { get; set; }
        public string usuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Publicacion Publicacion { get; set; }
    }
}
