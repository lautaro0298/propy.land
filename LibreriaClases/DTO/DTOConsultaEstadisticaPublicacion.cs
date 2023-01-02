using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOConsultaEstadisticaPublicacion
    {
        public List<DTOEstadisticaPublicacion> estadisticas { get; set; }
        public string ubicacionPropiedad { get; set; }
        public string publicacionId { get; set; }
        public DTOConsultaEstadisticaPublicacion() {
            estadisticas = new List<DTOEstadisticaPublicacion>();
        }
    }
}
