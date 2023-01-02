using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTO_TP_Y_TA
    {
        public List<DTOTipoAmbiente> dTOTipoAmbientes { get; set; }
        public List<DTOTipoPropiedad> dTOTipoPropiedades { get; set; }
        public DTO_TP_Y_TA()
        {
            dTOTipoAmbientes = new List<DTOTipoAmbiente>();
            dTOTipoPropiedades = new List<DTOTipoPropiedad>();
        }
    }
}
