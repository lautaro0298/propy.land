using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOCaracteristica
    {
        public string caracteristicaId { get; set; }
        public string nombreCaracteristica { get; set; }
        public bool ischeck { get; set; }
        public virtual List<DTO_TP_Y_C> tipoPropiedadCaracteristicas { get; set; }
    }
}
