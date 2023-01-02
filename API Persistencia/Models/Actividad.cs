using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class Actividad
    {
        public string actividadId { get; set; }
        public DateTime fechaActividad { get; set; }
        public string descripcionActividad { get; set; }
        public string usuarioId { get; set; }
    }
}
