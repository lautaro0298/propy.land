using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class NoPersistidoPlanUsuarioVisita
    {
        public PlanUsuario planUsuario { get; set; }
        public VisitaInmueble visita { get; set; }
    }
}
