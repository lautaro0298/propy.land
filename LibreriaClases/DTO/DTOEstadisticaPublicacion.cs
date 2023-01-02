using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOEstadisticaPublicacion
    {
        public string IDUsuarioVisitante { get; set; }
        public string nombreVisitante { get; set; }
        public int cantidadVisitas { get; set; }
        public string fechaUltimaVisita { get; set; }
        public bool solicitoDatosPublicante { get; set; }
        public bool usuarioVisitantePermiteSerContactadoPorPublicante { get; set; }
    }
}
