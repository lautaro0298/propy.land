using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTO_TP_Y_C
    {
        public string tipoPropiedadCaracteristicaID { get; set; }
        public string caracteristicaId { get; set; }
        public string propiedadId { get; set; }
        public bool activo { get; set; }
        public DTOCaracteristica dTOCaracteristicas { get; set; }
        public DTOTipoPropiedad dTOTipoPropiedades { get; set; }
      
    }
}
